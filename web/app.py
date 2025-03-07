from flask import Flask, render_template
import requests
import os

# Application Insights
import logging
# Import the `configure_azure_monitor()` function from the
# `azure.monitor.opentelemetry` package.
from azure.monitor.opentelemetry import configure_azure_monitor
from opentelemetry.sdk.resources import Resource

# Define a custom resource with the service name (maps to cloud_RoleName)
custom_resource = Resource(attributes={
    "service.name": "Globalmantics Books Web App"  # This sets the Cloud Role Name in Application Insights
})

# Configure OpenTelemetry to use Azure Monitor with the 
# if APPLICATIONINSIGHTS_CONNECTION_STRING environment variable is not null or empty, configure monitoring
if os.getenv("APPLICATIONINSIGHTS_CONNECTION_STRING"):
    configure_azure_monitor(
        logger_name="globalmantics.web",  # Set the namespace for the logger in which you would like to collect telemetry for if you are collecting logging telemetry. This is imperative so you do not collect logging telemetry from the SDK itself.
        resource=custom_resource,  # Set the resource name for the telemetry. This is imperative so you can identify the telemetry data coming from your application.
    )
    logger = logging.getLogger("globalmantics.web")  # Logging telemetry will be collected from logging calls made with this logger and all of it's children loggers.

app = Flask(__name__)

# Get the URL of the books API from the environment variable
books_api_url = os.getenv('BOOKS_API_URL', 'http://localhost:5000')
# Get the prefix_url_path, this is used to add a prefix to the URL path
prefix_url_path = os.getenv('PREFIX_URL_PATH', '') 

@app.route(f'{prefix_url_path}/')
def home():
    #logger.info("Fetching books to find the latest one")
    try:
        response = requests.get(f'{books_api_url}/books', timeout=5)
        response.raise_for_status()
    except requests.exceptions.RequestException as e:
        #logger.error(f"Error fetching books from API: {e}")
        return "Error fetching books from API", 500

    if response.status_code == 200:
        books = response.json()
        latest_book = max(books, key=lambda book: book['published'])
        #logger.info("Latest book details:", latest_book)
    else:
        return "Books not found", 404

    return render_template('home.html', book=latest_book)

@app.route(f'{prefix_url_path}/details/<int:book_id>')
def details(book_id):
    #logger.info("Getting book details for book id: ", book_id)
    response = requests.get(f'{books_api_url}/books/{book_id}')
    if response.status_code == 200:
        book = response.json()
        print(book)
    else:
        return "Book not found", 404
    return render_template('details.html', book=book)

@app.route(f'{prefix_url_path}/catalog')
def catalog():
    response = requests.get(f'{books_api_url}/books')
    if response.status_code == 200:
        books = response.json()
    else:
        books = []
    print(books)
    return render_template('catalog.html', books=books)

if __name__ == '__main__':
    app.run(debug=True)
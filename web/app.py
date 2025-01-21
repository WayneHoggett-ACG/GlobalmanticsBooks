from flask import Flask, render_template
import requests
import os

app = Flask(__name__)

books_api_url = os.getenv('BOOKS_API_URL', 'http://localhost:5154')

@app.route('/')
def home():
    print("Fetching books to find the latest one")
    response = requests.get(f'{books_api_url}/books')
    if response.status_code == 200:
        books = response.json()
        latest_book = max(books, key=lambda book: book['published'])
        print("Latest book details:", latest_book)
    else:
        return "Books not found", 404

    return render_template('home.html', book=latest_book)


@app.route('/details/<int:book_id>')
def details(book_id):
    print("Getting book details for book id: ", book_id)
    response = requests.get(f'{books_api_url}/books/{book_id}')
    if response.status_code == 200:
        book = response.json()
        print(book)
    else:
        return "Book not found", 404
    return render_template('details.html', book=book)

@app.route('/catalog')
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
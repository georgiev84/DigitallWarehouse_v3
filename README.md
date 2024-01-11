# DigitallWarehouse

Usage
Parameters:
- MinPrice - filter products by minimal price
- MaxPrice - filter products by maximal price
- Size - filter products by size
- Highlight - higlights the words in Description which are passed from Highlight parameter
- 
Example Requests:
GET /api/filter
- Returns all products from the database

GET /api/filter?MinPrice=10&MaxPrice=20&Highlight=yellow&Size=Small
- Returns products with minimal price of 10, maximal price of 20, size Medium and highlights the word 'yellow' in Description

Example Response:
{
  "filter": {
    "minPrice": 14.99,
    "maxPrice": 79.99,
    "allSizes": [
      "Small",
      "Medium",
      "Large"
    ],
    "commonWords": [
      "stylish",
      "casual",
      "to",
      "red",
      "blue",
      "look",
      "green",
      "white",
      "occasions",
      "yellow"
    ]
  },
  "products": [
    {
      "title": "Cozy Hoodie",
      "price": 16.99,
      "sizes": [
        "Small",
        "Medium"
      ],
      "description": "Warm and cozy hoodie in green for a relaxed and comfortable style."
    },
    {
      "title": "Leather Jacket",
      "price": 14.99,
      "sizes": [
        "Small",
        "Large"
      ],
      "description": "Stylish leather jacket in green for a bold and edgy fashion statement."
    },
    {
      "title": "Summer Shorts",
      "price": 16.99,
      "sizes": [
        "Small",
        "Medium"
      ],
      "description": "Light and breathable summer shorts in <em>yellow</em> for a cool and casual look."
    }
  ]
}

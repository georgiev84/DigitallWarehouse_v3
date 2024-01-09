# DigitallWarehouse

Usage
Parameters:
- MinPrice - filter products by minimal price
- MaxPrice - filter products by maximal price
- Size - filter products by size
- Highlight - higlights the words in Description which are passed from Highlight parameter
- 
Examples:
/filter
- Returns all products from the database

/filter?MinPrice=10&MaxPrice=89&Highlight=red,blue&Size=Medium
- Returns products with minimal price of 10, maximal price of 89, size Medium and highlights the words red and blue in Description

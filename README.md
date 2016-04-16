# webapi-cart
Experimenting with Microsoft's Web API. Small REST API with support for adding and removing products to cart.

# Usage
List products and carts:
* http://localhost:8080/products
* http://localhost:8080/carts

Support for GET, POST, PUT and DELETE

# Configuration
* Default storage is in memory (generates carts and products at startup)
* Support for MongoDB (toggle active factory in ComponentRegistry.cs)


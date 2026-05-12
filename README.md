# Tualla_Bien_EnhanceShoppingCartActivity

This activity is about making a simple shopping program using C#. In the Part 1, it could only add items to the cart but had no way to remove, update, or clear them. It also had no payment system, so the program never asked how much money the customer paid. There was no receipt number or date, and the receipt only showed the total without any order record. Part 1 also had no way to track previous transactions once the program moved on.

The Part 2 now has a full Main Menu where users can browse, search, filter by category, manage their cart, and view order history all in one place. The cart management lets users view, remove, update quantity, clear, or checkout their items, and the checkout process validates payment, computes the change, and prints a complete receipt with a number and date. After every checkout, the program automatically shows a low stock alert for products running low and saves the transaction to an order history that the user can review anytime.

AI Usage Section

AI Used: Claude AI

Prompts or Questions Asked to AI:
1. Here's my code, guide me on how I put getters and setters in it.
2. In this code, fix all of the bugs and errors. Don't change everything, just fix some errors and bugs.

I asked AI to guide me on how to add getters and setters to my code, so it helped me change all the public fields in the Product, CartItem, and Order classes into private fields with proper properties that also check for invalid values like negative numbers. I also asked AI to find and fix the bugs in my code without changing anything else, and it found problems like the duplicate class Program, the missing Category field, missing RestoreStock method, and the missing CartItem and Order classes. AI was only used to fix errors and add getters and setters, while the main logic and overall code were still made by me.

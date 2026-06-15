Use POSDB;

-- Insert 10 Categories
INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Devices and gadgets including smartphones, laptops, and accessories'),
('Clothing', 'Apparel for men, women, and children'),
('Books', 'Fiction and non-fiction books, textbooks, and magazines'),
('Home & Garden', 'Furniture, decor, and gardening supplies'),
('Sports & Outdoors', 'Sports equipment and outdoor gear'),
('Beauty & Health', 'Cosmetics, skincare, and health products'),
('Toys & Games', 'Toys, board games, and video games'),
('Automotive', 'Car parts, accessories, and maintenance products'),
('Food & Beverages', 'Groceries, snacks, and drinks'),
('Office Supplies', 'Stationery, paper products, and office equipment');

-- Insert 50 Products
INSERT INTO Products (Name, Barcode, Price, StockQuantity, CategoryId, IsActive) VALUES
-- Electronics (CategoryId: 1)
('iPhone 15 Pro', '8801641971234', 999.99, 25, 1, 1),
('Samsung Galaxy S24', '8801642156789', 849.99, 18, 1, 1),
('Sony WH-1000XM5 Headphones', '4905524930012', 349.99, 42, 1, 1),
('MacBook Air M2', '8859099501234', 1099.99, 15, 1, 1),
('Apple Watch Series 9', '190199274567', 399.99, 30, 1, 1),

-- Clothing (CategoryId: 2)
('Men''s Casual T-Shirt', '615123456789', 24.99, 100, 2, 1),
('Women''s Yoga Pants', '615987654321', 39.99, 75, 2, 1),
('Winter Jacket', '615456789123', 89.99, 40, 2, 1),
('Running Shoes', '615789123456', 79.99, 60, 2, 1),
('Baseball Cap', '615321654987', 19.99, 120, 2, 1),

-- Books (CategoryId: 3)
('The Great Gatsby', '9780743273565', 12.99, 50, 3, 1),
('Atomic Habits', '9780735211292', 16.99, 35, 3, 1),
('Python Programming Guide', '9781593279929', 49.99, 20, 3, 1),
('Cookbook: Mediterranean Diet', '9781984824865', 24.99, 30, 3, 1),
('Children''s Story Collection', '9780064400581', 14.99, 45, 3, 1),

-- Home & Garden (CategoryId: 4)
('Ceramic Plant Pot', '888750123456', 15.99, 80, 4, 1),
('LED Desk Lamp', '888750654321', 29.99, 55, 4, 1),
('Garden Hose 50ft', '888750789123', 34.99, 25, 4, 1),
('Coffee Table', '888750456789', 149.99, 12, 4, 1),
('Bed Sheet Set', '888750321654', 49.99, 40, 4, 1),

-- Sports & Outdoors (CategoryId: 5)
('Yoga Mat', '745687123456', 29.99, 65, 5, 1),
('Tennis Racket', '745687654321', 89.99, 22, 5, 1),
('Camping Tent 4-Person', '745687789123', 199.99, 18, 5, 1),
('Dumbbell Set 20kg', '745687456789', 79.99, 30, 5, 1),
('Bicycle Helmet', '745687321654', 39.99, 45, 5, 1),

-- Beauty & Health (CategoryId: 6)
('Vitamin C Serum', '360553123456', 24.99, 90, 6, 1),
('Electric Toothbrush', '360553654321', 59.99, 38, 6, 1),
('Face Moisturizer', '360553789123', 18.99, 70, 6, 1),
('Multivitamin Supplement', '360553456789', 14.99, 120, 6, 1),
('Hair Dryer', '360553321654', 44.99, 28, 6, 1),

-- Toys & Games (CategoryId: 7)
('LEGO Classic Set', '673419123456', 39.99, 55, 7, 1),
('Monopoly Board Game', '673419654321', 29.99, 40, 7, 1),
('Remote Control Car', '673419789123', 49.99, 32, 7, 1),
('Jigsaw Puzzle 1000pc', '673419456789', 19.99, 60, 7, 1),
('Play-Doh 10 Pack', '673419321654', 14.99, 85, 7, 1),

-- Automotive (CategoryId: 8)
('Car Phone Holder', '850008123456', 16.99, 95, 8, 1),
('Synthetic Motor Oil 5W-30', '850008654321', 34.99, 48, 8, 1),
('Car Wash Kit', '850008789123', 29.99, 35, 8, 1),
('Jump Starter Power Bank', '850008456789', 89.99, 20, 8, 1),
('Air Freshener Pack', '850008321654', 9.99, 150, 8, 1),

-- Food & Beverages (CategoryId: 9)
('Organic Coffee Beans', '811345123456', 14.99, 110, 9, 1),
('Dark Chocolate Bar', '811345654321', 4.99, 200, 9, 1),
('Olive Oil Extra Virgin', '811345789123', 19.99, 65, 9, 1),
('Granola Cereal', '811345456789', 6.99, 85, 9, 1),
('Green Tea Bags 100ct', '811345321654', 12.99, 95, 9, 1),

-- Office Supplies (CategoryId: 10)
('Wireless Mouse', '035000123456', 24.99, 75, 10, 1),
('Sticky Notes 10 Pack', '035000654321', 8.99, 180, 10, 1),
('Ballpoint Pens 12 Pack', '035000789123', 9.99, 220, 10, 1),
('Desk Organizer', '035000456789', 19.99, 42, 10, 1),
('Printer Paper 500 Sheets', '035000321654', 6.99, 160, 10, 1),

-- Additional Electronics
('iPad Air', '8859099512345', 599.99, 22, 1, 1),
('Wireless Earbuds', '4905524930023', 129.99, 50, 1, 1),

-- Additional Clothing
('Denim Jeans', '615123456780', 59.99, 45, 2, 1),
('Wool Sweater', '615987654322', 49.99, 35, 2, 1),

-- Additional Home & Garden
('Patio Chair', '888750123457', 79.99, 20, 4, 1),
('Cookware Set', '888750654322', 199.99, 15, 4, 1);
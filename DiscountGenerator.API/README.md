# gRPC approach

It consists in two services: one for generating discount codes and another one to apply those discount codes.

The generated codes are saved in a SQL Server database. This project uses EF Core as ORM.

This solution has a client project used for testing...
This client is performing the following actions:

 1. Create discount codes
 2. Get the last one from DB
 3. Apply this last discount code.

Applied codes are deleted from DB.

creating some discount codes (you can modify the quantity of codes and their length).
allowed lengths are: 7 or 8.

# Running the solution
The solution starts two projects simultaneously: The API and the client.

So, when running the solution the client will perform the actions mentioned above. 

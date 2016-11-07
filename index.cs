//1. Provide a query showing Customers (just their full names, customer ID and country) who are not in the US.
Select CustomerId as "id",
       FirstName as "First Name", 
       LastName as "Last Name"
from Customer
//2. Provide a query only showing the Customers from Brazil.
Select CustomerId,
       FirstName as "First Name", 
	     LastName as "Last Name",
	     Country  
from Customer 
where Country = "Brazil"
//3. Provide a query showing the Invoices of customers who are from Brazil. The resultant table should show the customer's full name, Invoice ID, Date of the invoice and billing country.
Select InvoiceId as "Invoice Id",
       FirstName as "First Name", 
	   LastName as "Last Name",
	   BillingCountry as "Billing Country",
	   InvoiceDate as "Date of Invoice" 
from Customer C
	join Invoice I
	on C.CustomerId = I.CustomerId
where Country = "Brazil"
//4. Provide a query showing only the Employees who are Sales Support Agents
Select FirstName,
	LastName,
	Title
from Employee
where Title = "Sales Support Agent"
//5. Provide a query showing a unique/distinct list of billing countries from the Invoice table.
Select Distinct BillingCountry
from Invoice
//6. Provide a query that shows the invoices associated with each sales agent. The resultant table should include the Sales Agent's full name.
Select E.FirstName as "Employee First Name",
	E.LastName as "Employee Last Name",
	InvoiceId
from Customer C
   join Employee E
   on SupportRepId = EmployeeId
   join Invoice I
   on C.CustomerId = I.CustomerId
//7. Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name for all invoices and customers.
Select C.FirstName as "Customer First Name",
	C.LastName as "Customer Last Name",
	E.FirstName as "Sale Agent First Name",
	E.LastName as "Sale Agent Last Name",
	C.Country,
	Total as "Invoice Total"
from Customer C
   join Employee E
   on SupportRepId = EmployeeId
   join Invoice I
   on C.CustomerId = I.CustomerId
//8. Provide a query to show how many Invoices there were in 2009 and in 2011
Select InvoiceDate
from Invoice
Where(InvoiceDate between "2009-01-01 00:00:00" and "2009-12-31 00:00:00")
or (InvoiceDate between "2011-01-01 00:00:00" and "2011-12-31 00:00:00")
//Or the cleaner version:
Select InvoiceDate
from Invoice
Where InvoiceDate LIKE "2009%"
or InvoiceDate LIKE "2011%"
//9. What are the respective total sales for each of those years?
select (
select Count(*) from Invoice where InvoiceDate like "2009%" ) as TotalInvoicesIn2009, 
(select Count(*) from Invoice where InvoiceDate like "2011%")  as TotalInvoicesIn2011 
//10. Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for Invoice ID 37.
Select Count(InvoiceLineId)
from InvoiceLine 
Where InvoiceId = 37
//11. Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for each Invoice.
Select I.InvoiceId as "Invoice Id",
       Count(InvoiceLineId) as "Number Of Line Items"
from InvoiceLine IL
join Invoice I
on I.InvoiceId = IL.InvoiceId
Group by I.InvoiceId
//12. Provide a query that includes the purchased track name with each invoice line item.
Select InvoiceLineId as "Invoice Line Id",
       T.Name as "Track Name"
from InvoiceLine IL
join Track T
on T.TrackId = IL.TrackId
//13. Provide a query that includes the purchased track name AND artist name with each invoice line item.
Select InvoiceLineId as "Invoice Line Id",
       T.Name as "Track Name",
	     AR.Name as "Artist Name"
from InvoiceLine IL
join Track T
on T.TrackId = IL.TrackId
join Album A
on A.AlbumId = T.AlbumId
join Artist AR
on AR.ArtistId=A.ArtistId
//14. Provide a query that shows the # of invoices per country.
Select BillingCountry,
       Count(InvoiceId) as "Number of Invoices in This Country"
from Invoice
group by BillingCountry
//15. Provide a query that shows the total number of tracks in each playlist. The Playlist name should be include on the resulant table
Select P.Name,
       Count(PT.TrackId) as "Number of Tracks in this Playlist"
from Playlist P
join PlaylistTrack PT
on P.PlaylistId = PT.PlaylistId
group by P.Name
//16. Provide a query that shows all the Tracks, but displays no IDs. The result should include the Album name, Media type and Genre
Select A.Title as "Album Name",
	   M.Name as "Media Type",
	   G.Name as "Genre"
from Track T
join Genre G
on G.GenreId = T.GenreId
join MediaType M
on M.MediaTypeId = T.MediaTypeId
join Album A
on A.AlbumId = T.AlbumId
//17. Provide a query that shows total number of invoices sold by each sales agent.
Select E.FirstName as "Employee First Name",
       E.LastName as "Employee Last Name",
      Count(InvoiceId) as "Number of Invoices"
from Employee E
join Customer C
on SupportRepId = EmployeeId
join Invoice I
on C.CustomerId = I.CustomerId
group by E.FirstName, E.LastName
//18. Provide a query that shows the total number of sales made by each sales agent 
Select E.FirstName as "Employee First Name",
       E.LastName as "Employee Last Name",
       Sum(Total) as "Total Sales ($)"
from Employee E
join Customer C
on SupportRepId = EmployeeId
join Invoice I
on C.CustomerId = I.CustomerId
group by E.FirstName, E.LastName
//19. Which sales agent made the most in sales in 2009
select E.FirstName, 
    E.LastName,
	SUM(Total) as "Number of Sales"
from Employee E
join Customer C
on SupportRepId = EmployeeId
join Invoice I
on I.CustomerId = C.CustomerId
where InvoiceDate like "2009%"
group by E.FirstName, E.LastName
order by "Number of Sales" desc
limit 1
//20. Which sales agent made the most in sales over all?
Select E.FirstName as "Employee First Name",
E.LastName as "Employee Last Name",
Sum(Total) as "Invoice Total"
from Employee E
join Customer C
on SupportRepId = EmployeeId
join Invoice I
on C.CustomerId = I.CustomerId
group by E.FirstName, E.LastName
//21. Provide a query that shows the count of customers assigned to each sales agent.
Select E.FirstName as "Employee First Name",
E.LastName as "Employee Last Name",
Count(CustomerId) as "Number of Customers"
from Employee E
join Customer C
on SupportRepId = EmployeeId
group by E.FirstName, E.LastName
//22. Provide a query that shows the total sales per country.
Select Country as "Country",
Sum(Total) as "Total Sales in Dollars"
from Invoice I
join Customer C
on C.CustomerId = I.CustomerId
group by Country
//23. Which country's customers spent the most?
select Country, 
	SUM(Total) as "Number of Sales"
from Customer C
join Invoice I
on C.CustomerId = I.CustomerId
group by Country
order by "Number of Sales" desc
limit 1
//24. Provide a query that shows the most purchased track of 2013.
select T.Name, 
	COUNT(InvoiceLineId) as "Number of Sales"
from Track T
join InvoiceLine IL
on IL.TrackId = T.TrackId
join Invoice I
on I.InvoiceId = IL.InvoiceId
where InvoiceDate like "2013%"
group by T.Name
order by "Number of Sales" desc
limit 1
//25. Provide a query that shows the top 5 most purchased tracks over all.
select T.Name, 
	COUNT(InvoiceLineId) as "Number of Sales"
from Track T
join InvoiceLine IL
on IL.TrackId = T.TrackId
join Invoice I
on I.InvoiceId = IL.InvoiceId
group by T.Name
order by "Number of Sales" desc
limit 5
//26. Provide a query that shows the top 3 best selling artists.
select A.Name, 
	COUNT(InvoiceLineId) as "Number of Sales"
from Artist A
join Album AL
on AL.ArtistId = A.ArtistId
join Track T
on T.AlbumId = AL.AlbumId
join InvoiceLine IL
on IL.TrackId = T.TrackId
group by A.Name
order by "Number of Sales" desc
limit 3
//27. Provide a query that shows the most purchased Media Type.
select M.Name, 
	COUNT(InvoiceLineId) as "Number of Sales"
from MediaType M
join Track T
on T.MediaTypeId = M.MediaTypeId
join InvoiceLine IL
on IL.TrackId = T.TrackId
group by M.Name
order by "Number of Sales" desc
limit 1

#!/bin/sh

rm -fr Migrations
rm -f sqlite/okmusic.db

dotnet ef migrations add InitialCreate
dotnet ef database update

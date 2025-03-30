#!/bin/bash

set -e  # Avsluta om något kommando misslyckas

# Skapa en ny solution
dotnet new sln -n FirstSubmission

# Skapa projektmappen och testmappen
mkdir -p FirstSubmission FirstSubmission.Tests

# Skapa ett Class Library-projekt
echo "Skapar Class Library..."
cd FirstSubmission
dotnet new classlib
cd ..

# Lägg till projektet i solution
dotnet sln add FirstSubmission

# Skapa ett MSTest-projekt
echo "Skapar MSTest-projekt..."
cd FirstSubmission.Tests
dotnet new mstest

# Lägg till en referens till Class Library-projektet
dotnet add reference ../FirstSubmission
cd ..

# Lägg till testprojektet i solution
dotnet sln add FirstSubmission.Tests

# Återställ beroenden
dotnet restore

# Bygg projektet
dotnet build

# Kör tester
echo "Kör tester..."
dotnet test

echo "Solution och projekt är klara!"

# Vänta på användaren att trycka på en tangent
read -p "Tryck på en valfri tangent för att avsluta..."

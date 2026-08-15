# Quiz Builder 🎯

A standalone desktop quiz management application built with **C# and WPF (.NET)**. Quiz Builder allows teachers to create and manage multiple-choice question banks while students can attempt quizzes, receive automatic scores, and view their results.

The application works completely **offline** and uses **XML serialization** for permanent data storage instead of requiring a database.

## ✨ Features

### Question Management

* Create multiple-choice questions
* Edit existing questions
* Delete questions
* View the complete question bank
* Search questions using keywords
* Filter questions by topic
* Filter questions by difficulty
* Prevent duplicate questions

### Quiz System

* Generate quizzes with randomly selected questions
* Attempt multiple-choice quizzes
* Automatic answer checking
* Automatic score and percentage calculation
* Display detailed quiz results

### Result Management

* View quiz summaries
* View correct and incorrect answers
* Export quiz results as CSV

### Data Storage

* XML-based permanent data storage
* Automatically load saved questions when the application starts
* Save quiz and result information
* No database required

## 🛠️ Technologies Used

| Technology        | Purpose                                            |
| ----------------- | -------------------------------------------------- |
| **C#**            | Application logic                                  |
| **WPF (.NET)**    | Desktop user interface                             |
| **XAML**          | Window and control design                          |
| **XML**           | Permanent data storage                             |
| **LINQ**          | Searching, filtering, sorting, and quiz generation |
| **Async/Await**   | Asynchronous file operations                       |
| **Visual Studio** | Development environment                            |

## 🧠 Programming Concepts Demonstrated

This project demonstrates several C# and software development concepts:

* Object-Oriented Programming (OOP)
* CRUD operations
* LINQ
* XML serialization
* Asynchronous programming
* Event handling
* Exception handling
* Input validation
* Modular application architecture

The main classes include `Question`, `Quiz`, `Result`, `AnswerRecord`, `XmlDataService`, and `QuestionFilterService`.

## 🔎 LINQ

LINQ is used for:

* Searching questions by keyword
* Filtering by topic
* Filtering by difficulty
* Selecting random quiz questions
* Displaying filtered records

Methods used include:
     Where()
     Contains()
     OrderBy()
     Take()
     ToList()

## 💾 Data Storage

Quiz Builder uses XML serialization rather than a database.

The application uses XML files for storing:

* Questions
* Quiz results
* Quiz information

XML provides permanent, human-readable storage without requiring database installation.

## ⚡ Asynchronous Programming

File operations use:
     async
     await
     Task.Run()
This helps prevent the WPF interface from freezing during file operations and keeps the application responsive.

## 🏗️ Architecture

The project follows a modular structure using separate **Models, Services, and Views** to improve maintainability and future development.

## ▶️ How to Run

1. Clone the repository.
2. Open `QuizBuilder.sln` in Visual Studio.
3. Make sure the required .NET environment is installed.
4. Build the solution.
5. Run the application.

The application is designed to run on Windows with the required .NET runtime installed.

## 🧪 Testing

The application was tested for:

* Valid and invalid input
* Empty input
* Duplicate questions
* Search functionality
* Add, Edit, and Delete operations
* XML saving and loading
* Quiz scoring
* Result export
* Data persistence after application restart

## 🚀 Future Enhancements

Possible future improvements include:

* User authentication and role management
* SQL Server or SQLite integration
* Quiz timer
* Images and multimedia questions
* Online quiz functionality
* Student profiles and history
* Performance analytics
* Question import/export
* Automatic backup
* Cloud synchronization
* PDF result generation

## 📌 Project Status

**Completed academic project**

Developed as an Advanced Programming project to demonstrate practical application of C#, WPF, OOP, CRUD, LINQ, XML serialization, asynchronous programming, and other software development concepts.

# Angular.NET

**This is a simple prototype project called "RFID-Management"**
The purpose of this project is a demonstration of:
- Setting up .NET6 Angular project
- Microservices and Dependency Injection
- Entity Framework (SQLite local database) usage
- WebAPI and Swagger usage 
- Web part (Typescript / HTML / CSS) altough im not much experienced with it

_Software is created in Visual Studio 2022 preview_

**Project setup in Visual Studio 2022**

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/41f00880-72e9-4206-b087-decf5cc7485f)

**How software works?**
Software runs node.js to compile the web scripts and it runs locally on port 44498.

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/d21f57f5-d7c7-4215-b7c6-afac2920312f)

As this is a prototype (demonstration) it comes with a basic UI which allows CRUD operations on RFID allowed users and it has 
no authentication which can be additionaly added. Of course, project has been set-up to be easily extended depending of what user needs.
Be advised that the purpose of this prototype project was not "styling" of UI. It can be easily done later.

In details, UI communicates with the backend server which communicates with the Entity Framework to provide data.

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/9cb5e757-f12b-4bb2-9fc4-5c42aa566ff6)

Also, Swagger is available on port 7274 (https://localhost:7274/swagger/index.html).

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/54f17ca5-4b60-418f-91f3-4749d1a08d9a)

Backend runs 'DatabaseService" and "ReceiverService"
- Database service operates Entity Framework for performing CRUD operations on users
- Receiver services is a simple TCP listener which intercepts and handles the incoming TCP data

**Testing of TCP client connection**

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/d0124add-12ed-4ae1-9ebb-9e4dea2793cb)

**Results**

![image](https://github.com/NightRider92/RFID-Management/assets/10942663/15ca2b02-e44d-43ae-9a2e-3319b0f88925)

Project has been done in approximately ~ 12 hours from the scratch.

Project can be easily be extended to operate RS-232 or RS-485 (e.g. on Linux embedded device) to receive the RFID-values and to 
operate GPIOs to perform e.g. "door-opening" or similar things.




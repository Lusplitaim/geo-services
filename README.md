# Arc GIS App

A simple app that allows to use Belarus map and manage proxy server to modify access rules.

## Running the project with Docker

To run the app with docker you need to:
1. Open root directory;
2. Run `docker compose up --build`.

After that follow `http://localhost:3000` link. By default you can access geo-services 10 times (10 times loading the page),
the browser will receive 403 error if that number will be exceeded.

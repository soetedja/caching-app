# **Web Caching App**

This simple web application uses the Least Recently Used (LRU) algorithm to demonstrate caching. The backend is implemented in C# and uses the `ConcurrentDictionary` data structure to handle the cache. The front end is implemented using Angular.

## **Getting Started**

### **Installing**

1. Clone this repository:
    
    ```bash
    git clone https://github.com/soetedja/caching-app.git
    ```
    
2. Navigate to the project directory:
    
    ```bash
    cd caching-app/backend/CachingApp
    ```
    
3. Install the backend dependencies:
    
    ```bash
    dotnet restore
    ```
    
4. Install the frontend dependencies:
    
    ```bash
    cd user-cache-app
    npm install
    ```
    

### **Running the Application**

1. Start the backend server:
    
    ```bash
    dotnet run
    ```
    
    Or open it from Visual studio and Start the `WebServer` app
    
    The server should now be running on **`http://localhost:7168`**.
    
2. Start the frontend client:
    
    ```
    ng serve
    ```
    
    The client should now be running on **`http://localhost:4200`**.
    
 ## **Using the Application**

The UI consists of the following components:

- **Input Number of Users**
    
    This input field allows you to specify the number of users to generate.
    
- **Generate Users Button**
    
    Clicking this button generates the specified number of users and calls the backend API.
    
- **Cache Statistics**
    
    This component displays the cache statistics as a raw JSON object.
    
- **Cache Visualization**
    
    This component visualizes the cache as a block memory.
    
- **Request Queue Status**
    
    This component shows the status of the request queue.
    
- **Update Request Settings**
    
     This component allows you to update the request settings.
    
- **Auto Update Visualization**
    
    This toggle switch turns on/off the automatic update of the cache visualization.
    
- **Clear Cache Button**
    
    Clicking this button clears the cache.

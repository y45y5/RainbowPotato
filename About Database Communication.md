# How does the DAO, Cache and Repository work?

## 1. Cache Layer

**Class: `CustomCache<T>`**

- **Responsibilities**: 
  - This class manages the in-memory caching of objects using the `MemoryCache` from C#.
  
- **Methods**:
  - `AddToCache(T model, string cacheKey)` – Adds a model to the cache with a specific key.
  - `GetFromCache(string cacheKey)` – Tries to retrieve a model from the cache by the provided `cacheKey`. If the key does not exist, it throws a `NotInCacheException`.
  - `RemoveFromCache(string cacheKey)` – Removes an entry from the cache by its key.
  - `Clear()` – Clears all the cached items from the memory.

- **Main Parameters**:
  - `cacheKey` – A string key used to identify each item in the cache.
  - `T model` – The object model that will be stored in the cache.

## 2. DAO (Data Access Object) Layer

**Class: `Dao<T>`**

- **Responsibilities**:
  - This class is responsible for interacting with MongoDB, handling the retrieval and saving of data to the database.

- **Methods**:
  - `GetMongoClient()` – Creates and returns an instance of the MongoDB client using the connection string.
  - `GetCollection(string collectionName)` – Retrieves a MongoDB collection based on the name of the collection.
  - `GetResultFromDatabase(string cacheKey)` – Retrieves a single document from MongoDB based on the provided `cacheKey`. It filters the documents using `guildId` extracted from the `cacheKey`.
  - `GetResultsFromDatabase(string cacheKey)` – Retrieves all documents from the MongoDB collection.
  - `AddCleanRecordToDatabase(string cacheKey)` – Creates a new, clean record in the database, based on the `guildId` extracted from the `cacheKey`, and adds it to the MongoDB collection.

- **Main Parameters**:
  - `cacheKey` – A string used to derive both the collection name and the `guildId` for the database query.
  - MongoDB connection string (from settings).

## 3. Repository Layer

**Class: `MongoRepository<T>`**

- **Responsibilities**:
  - This class serves as the intermediary between the DAO and Cache layers. It handles data retrieval by first checking the cache and, if not found, querying the database. It also manages synchronization and caching of the results.

- **Methods**:
  - `GetResults(string cacheKey)`:
    1. Attempts to retrieve data from the cache using the `cacheKey`.
    2. If data is not found in the cache, it queries the database using the DAO's `GetResultFromDatabase()` method.
    3. If the data is also not in the database, it creates a new clean record using `AddCleanRecordToDatabase()` and then stores the result in the cache.
  - `ClearCache()` – Clears all data in the cache by calling `Clear()` in the cache layer.

- **Main Parameters**:
  - `IDao<T>` – An instance of the DAO layer responsible for interacting with MongoDB.
  - `ICustomCache<T>` – An instance of the cache layer that stores data in memory.
  - `cacheKey` – A string key used to identify records both in the cache and the database.

## 4. How the Layers Work Together (Step-by-Step)

1. **The user or another part of the code** invokes the `GetResults(string cacheKey)` method from the repository layer (`MongoRepository<T>`).
2. The repository first tries to **retrieve data from the cache** using the cache layer (`CustomCache<T>`). If the data exists, it is immediately returned.
3. If the data **is not in the cache**, the repository calls the `GetResultFromDatabase()` method in the DAO layer to retrieve the data from MongoDB.
4. If the data **does not exist in the database**, the DAO creates a new clean record using `AddCleanRecordToDatabase()` and stores it in the database.
5. After retrieving or creating the data, the repository **adds the data to the cache** using the `AddToCache()` method in the cache layer for faster future access.
6. The **data is then returned to the user** or calling function.
7. The `ClearCache()` method in the repository layer can be called to **clear all cached data**, which delegates the action to the cache layer's `Clear()` method.
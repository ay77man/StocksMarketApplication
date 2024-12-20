# Stock Trading Application

This project is an ASP.NET Core web application that provides a live stock trading platform using data from [Finnhub.io](https://finnhub.io/). The application enables users to view live stock prices, place buy/sell orders, and manage stock trades effectively.

## Table of Contents
- [Features](#features)
- [Architecture](#architecture)
- [Setup](#setup)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [API Integration](#api-integration)
- [Key Components](#key-components)
- [Unit Tests](#unit-tests)
- [Future Enhancements](#future-enhancements)
- [License](#license)

---

## Features
- **Live Stock Price Updates:** Real-time updates using the Finnhub WebSocket API.
- **Buy/Sell Orders:** Place buy and sell orders with validation and persistence.
- **Order Management:** View detailed lists of placed buy and sell orders.
- **Responsive UI:** Intuitive design with dynamic updates for stock prices.

---

## Architecture
The application follows an N-tier architecture:
1. **Presentation Layer:** Handles UI interactions and displays data.
2. **Service Layer:** Contains business logic and service methods.
3. **Data Access Layer:** Interfaces with the database for storing and retrieving data.
4. **External Integration:** Integrates with Finnhub.io for stock data.

---

## Setup

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or any preferred IDE
- [SQL Server](https://www.microsoft.com/sql-server) (optional, depending on the database setup)
- API key from [Finnhub.io](https://finnhub.io/login) (or use the provided demo key: `cc676uaad3i9rj8tb1s0`)

### Configuration
1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd StockTradingApplication
   ```

2. **Store the API Key in User Secrets:**
   ```bash
   dotnet user-secrets set "FinnhubToken" "your-api-key"
   ```

3. **Configure `appsettings.json`:**
   Update default trading options:
   ```json
   {
     "TradingOptions": {
       "DefaultStockSymbol": "MSFT",
       "DefaultOrderQuantity": 100
     }
   }
   ```

4. **Database Setup:**
   Ensure your database is set up and update the connection string in `appsettings.json` if required.

### Running the Application
- **Build and Run:**
  ```bash
  dotnet build
  dotnet run
  ```
- **Access the Application:** Navigate to `http://localhost:5000` in your browser.

---

## Project Structure
- **Controllers:** Manages HTTP requests and responses.
- **Services:** Handles business logic.
- **Models:** Includes entity models, DTOs, and view models.
- **Views:** Contains Razor views for UI rendering.
- **wwwroot:** Includes static files like CSS and JavaScript.
- **Unit Tests:** Tests business logic in service methods.

---

## API Integration

### Finnhub API
- **WebSocket URL:**
  `wss://ws.finnhub.io?token={token}`
- **REST Endpoints:**
  - **Company Profile:**
    `https://finnhub.io/api/v1/stock/profile2?symbol={symbol}&token={token}`
  - **Stock Price Quote:**
    `https://finnhub.io/api/v1/quote?symbol={symbol}&token={token}`

---

## Key Components

### Entity Models
- `BuyOrder`
- `SellOrder`

### DTO Models
- `BuyOrderRequest`, `BuyOrderResponse`
- `SellOrderRequest`, `SellOrderResponse`

### View Models
- `StockTrade`
- `Orders`

### Views
- **Index.cshtml:** Displays live stock prices and buy/sell options.
- **Orders.cshtml:** Lists all buy and sell orders.

---

## Unit Tests
Key test cases include:
- Validating `BuyOrderRequest` and `SellOrderRequest` inputs.
- Ensuring proper behavior of `CreateBuyOrder` and `CreateSellOrder` methods.
- Verifying the retrieval of buy/sell orders with `GetBuyOrders` and `GetSellOrders`.

---

## Future Enhancements
- Add user authentication and role-based access.
- Enable stock symbol search functionality.
- Implement advanced order types (e.g., limit orders).
- Enhance UI for better responsiveness and interactivity.

---

## License
This project is licensed under the MIT License. See `LICENSE` for details.


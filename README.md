# ExampleService

## 🏗 Kiến trúc tổng quan
`ExampleService` được xây dựng theo mô hình **Clean Architecture + DDD**, kết hợp **.NET 8**, **SQL Server**, **MongoDB**, **Postgres**, cùng các thư viện hỗ trợ như **Dapper**, **MassTransit**, **Hangfire Pro**, và **Ocelot API Gateway**.  

Solution gồm các **tầng chính**:

- **Applications** → Chứa các ứng dụng cụ thể (Consumer, JobScheduler, Publisher, WebApp).
- **BuildingBlocks** → Cung cấp các khối nền tảng dùng chung (Authentication, Database Context, Options/Config).
- **Gateways** → API Gateway quản lý routing giữa client và service (Ocelot).
- **Services** → Chứa các tầng Domain, Application, Infrastructure, Messaging, Validation, Abstraction, Helpers.

---

## 📂 Cấu trúc thư mục

### **Applications**
1. **Applications.Consumer**  
   - Consumer service dùng để nhận message từ Publisher thông qua MassTransit & RabbitMQ/Kafka.  

2. **Applications.JobScheduler**  
   - Sử dụng **Hangfire Pro** để quản lý background jobs (schedule, recurring jobs, delayed jobs).  

3. **Applications.Publisher**  
   - Service Publisher phát sự kiện/message ra queue, giao tiếp với Consumer.  

4. **Applications.WebApp**  
   - Web API chính, phục vụ request từ client.  
   - Tích hợp các BuildingBlocks và Services để expose business logic.  

---

### **BuildingBlocks**
1. **BuildingBlocks.Authentications**  
   - Quản lý **User Identity**.  
   - Sinh JWT token, xác thực, phân quyền.  
   - Được reference bởi toàn bộ **Applications**.  

2. **BuildingBlocks.DataBases**  
   - Chứa Database Context:  
     - SQL Server (EF Core hoặc Dapper).  
     - MongoDB Context.  
     - PostgreSQL Context.  

3. **BuildingBlocks.Options**  
   - Chứa các file cấu hình dùng chung (ví dụ: connection string, RabbitMQ config, JWT options).  
   - Tích hợp **Minimal API configuration**.  
   - Được reference bởi toàn bộ **Applications**.  

---

### **Gateways**
1. **Gateways.Ocelot**  
   - Reverse Proxy API Gateway.  
   - Quản lý route, load balancing, authentication/authorization.  
   - Config bằng file `appsettings.json`.  

---

### **Services**
1. **Services.Abstractions**  
   - Chứa interface định nghĩa cho tầng Infrastructure, Domain, Application.  

2. **Services.Application**  
   - Chứa enum, static config class, các cấu hình logic dùng chung.  

3. **Services.Domain**  
   - Chứa domain entity, aggregate root, value object theo DDD.  

4. **Services.Helpers**  
   - Chứa các file tiện ích (string helper, date helper, mapper, …).  

5. **Services.Infrastructure**  
   - Chứa các class implement repository, db context, external service client.  

6. **Services.Messaging**  
   - Chứa consumer, producer, định nghĩa message contracts cho MassTransit.  

7. **Services.Validations**  
   - Sử dụng **FluentValidation** để validate DTO, command, query.  

---

## 🛠 Công nghệ chính
- **.NET 8** → Core framework.  
- **SQL Server, MongoDB, PostgreSQL** → Data storage.  
- **Dapper** → ORM lightweight.  
- **Hangfire Pro** → Background job scheduler.  
- **MassTransit** → Message broker abstraction (RabbitMQ/Kafka).  
- **Ocelot** → API Gateway.  
- **FluentValidation** → Validation layer.  

---

## 🚀 Cách chạy
1. **Clone repo**  
   ```bash
   git clone https://github.com/your-org/ExampleService.git
   cd ExampleService
   ```

2. **Chạy database migrations (nếu có)**  

3. **Chạy Gateway**  
   ```bash
   dotnet run --project Gateways.Ocelot
   ```

4. **Chạy Web API chính**  
   ```bash
   dotnet run --project Applications.WebApp
   ```

5. **Chạy Publisher/Consumer**  
   ```bash
   dotnet run --project Applications.Publisher
   dotnet run --project Applications.Consumer
   ```

6. **Chạy JobScheduler (Hangfire Pro Dashboard)**  
   ```bash
   dotnet run --project Applications.JobScheduler
   ```

---

## 📌 Ghi chú
- Các dự án trong **Applications** và **Services** đều reference **BuildingBlocks** để dùng chung Authentication, Options, và Database context.  
- API Gateway (**Gateways.Ocelot**) chỉ thực hiện **reverse proxy** và không tham chiếu trực tiếp các service.  
- Các service giao tiếp với nhau thông qua **Messaging (MassTransit)** thay vì gọi trực tiếp.  

# ĐỒ ÁN LẬP TRÌNH AN TOÀN

## Yêu cầu:
-   Form đăng ký
-   Form đăng nhập
-   Form thi

## Cụ Thể:

### 1. Form Đăng Ký
-   Form đăng ký phải lưu vô Database và mã hóa.

### 2. Form Đăng Nhập
-   Form đăng nhập phải bảo vệ đc tấn công XSS, SQL Injection(trên cả user lẫn password), phải có mã hóa password trong database, password yêu cầu phải tối thiểu 8 chữ gồm chữ hoa chữ thường ký tự đặc biệt và số.
-   Có thể làm thêm xác thực MAC để đảm bảo an toàn cho đề thi.

### 3. Form Thi
-   Trong form thi phải có nút đổi password.
-   Nút soạn đề (soạn từng câu hỏi đưa vô ngân hàng đề), nút tạo đề thi lấy từ ngân hàng đề ra(tạo ra nhiều đề, mỗi đề có thể lấy các câu bất kì câu hỏi nào do người quản trị chọn, chỉ có người quản trị mới được vô chỗ soạn đề). Đề phải được bảo mật và mã hóa.
-   Nút thi (có thể chọn nhiều đề thi).

## Mô Tả Đồ Án:
-   Chỉ 1 Giảng Viên (Admin) – Thí Sinh (User + MSSV tương đương với mã đề) - Giảng Viên có sẵn tài khoản và chỉ có 1 tk Admin.
-   Thí Sinh chỉ có tài khoản sau khi Giảng Viên (Admin) tạo.
-   Pass tài khoản của GV và TS đều được mã hóa bằng Bcrypt – mã hóa 1 chiều( tích hợp trong C#) - FormSignup line 53 or FormChangePW Line 105.
-   Chức năng của Giảng Viên và Chức năng của Thí Sinh được tách biệt giữa 2 form:
    +   Chức năng đổi mật khẩu: Chỉ có thể thay đổi sau khi login, tối thiểu 8 và tối đa 15 ký tự, bao gồm ký tự đặc biệt và chữ cái viết hoa - FormChangePW line 59.
    +   Mã hóa câu hỏi (mã hóa 2 chiều): FormAddquestion line 100.
<!--
## Cài Đặt:
-   Mở App.config thay đổi tên Server SQL.
```
<?xml version="1.0" encoding="utf-8" ?>*
<configuration>
    <configSections>
    </configSections>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
  <connectionStrings>
    <add name="Test" connectionString="Data Source=DESKTOP-LRS30NK\SQL;
        Initial Catalog=QLDThi;Integrated Security=True;
        MultipleActiveResultSets=true;
        "providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>*
```
-   Mở QLDThi.sql trong thư mục và tạo DB.
-->

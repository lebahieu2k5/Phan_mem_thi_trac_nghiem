-- Bảng Tài Khoản
CREATE TABLE taikhoan (
    TenTaiKhoan VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(50),
    Email VARCHAR(50),
    LoaiTaiKhoan INT
);


-- Bảng Khoa
CREATE TABLE khoa (
    KhoaID INT PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)   -- Mô tả về khoa
);

CREATE TABLE sinhvien (
    SinhVienID INT PRIMARY KEY,
    TenTaiKhoan VARCHAR(50) NOT NULL,
    HoTen NVARCHAR(255) NOT NULL,
    Email VARCHAR(50),
    KhoaID INT,  -- Sinh viên sẽ thuộc một khoa
    FOREIGN KEY (TenTaiKhoan) REFERENCES taikhoan(TenTaiKhoan),  -- Liên kết với bảng Tài Khoản
    FOREIGN KEY (KhoaID) REFERENCES khoa(KhoaID)  -- Liên kết với bảng Khoa
);



CREATE TABLE giangvien (
    GiangVienID INT PRIMARY KEY,
    TenTaiKhoan VARCHAR(50) NOT NULL,
    HoTen NVARCHAR(255) NOT NULL,
    Email VARCHAR(50),
    KhoaID INT,  -- Giảng viên thuộc một khoa
    FOREIGN KEY (TenTaiKhoan) REFERENCES taikhoan(TenTaiKhoan),  -- Liên kết với bảng Tài Khoản
    FOREIGN KEY (KhoaID) REFERENCES khoa(KhoaID)  -- Liên kết với bảng Khoa
);




-- Bảng Môn Học
CREATE TABLE monhoc (
    MonID INT PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL
);


-- Bảng Đề Thi
CREATE TABLE dethi (
    DeThiID INT PRIMARY KEY,
    MonID INT NOT NULL,
    TenDeThi NVARCHAR(100) NOT NULL,
    SoCauHoi INT NOT NULL,
    NgayTao DATE NOT NULL,
    GiangVienID INT NOT NULL,
    KhoaID INT,  -- Liên kết với bảng Khoa (nếu cần phân quyền cho khoa)
    FOREIGN KEY (MonID) REFERENCES monhoc(MonID),
    FOREIGN KEY (GiangVienID) REFERENCES giangvien(GiangVienID),
    FOREIGN KEY (KhoaID) REFERENCES khoa(KhoaID)  -- Liên kết với bảng Khoa
);



-- Bảng Câu Hỏi
CREATE TABLE cauhoi (
    CauHoiID INT PRIMARY KEY,
    MonID INT NOT NULL,
    NoiDungCauHoi NVARCHAR(MAX) NOT NULL,
    DapAnA NVARCHAR(255),
    DapAnB NVARCHAR(255),
    DapAnC NVARCHAR(255),
    DapAnD NVARCHAR(255),
    DapAnDung CHAR(1),
    FOREIGN KEY (MonID) REFERENCES monhoc(MonID)
);


-- Bảng Chi Tiết Đề Thi
CREATE TABLE chitietdethi (
    ChiTietDeThiID INT PRIMARY KEY,
    DeThiID INT NOT NULL,
    CauHoiID INT NOT NULL,
    FOREIGN KEY (DeThiID) REFERENCES dethi(DeThiID),
    FOREIGN KEY (CauHoiID) REFERENCES cauhoi(CauHoiID)
);


-- Bảng Kết Quả Thi
CREATE TABLE ketquathi (
    KetQuaID INT PRIMARY KEY,
    DeThiID INT NOT NULL,
    SinhVienID INT NOT NULL,  -- Liên kết với bảng Sinh Viên
    DiemSo FLOAT,
    ThoiGianLam TIME,
    KhoaID INT,  -- Thay thế LopHocID bằng KhoaID
    GiangVienID INT,
    FOREIGN KEY (DeThiID) REFERENCES dethi(DeThiID),
    FOREIGN KEY (SinhVienID) REFERENCES sinhvien(SinhVienID),  -- Liên kết với bảng Sinh Viên
    FOREIGN KEY (GiangVienID) REFERENCES giangvien(GiangVienID),
    FOREIGN KEY (KhoaID) REFERENCES khoa(KhoaID)  -- Liên kết với bảng Khoa
);


-- Bảng Truy Cập Đề Thi
CREATE TABLE truycapdethi (
    TruyCapDeThiID INT PRIMARY KEY,
    SinhVienID INT NOT NULL,  -- Thay thế HocSinhID bằng SinhVienID
    DeThiID INT NOT NULL,
    TrangThaiTruyCap NVARCHAR(50),
    NgayTruyCap DATE,
    FOREIGN KEY (SinhVienID) REFERENCES sinhvien(SinhVienID),  -- Liên kết với bảng Sinh Viên
    FOREIGN KEY (DeThiID) REFERENCES dethi(DeThiID)
);



-- Bảng Phân Công
CREATE TABLE phancong (
    PhanCongID INT PRIMARY KEY,
    GiangVienID INT NOT NULL,
    MonID INT NOT NULL,
    KhoaID INT,  -- Thay thế LopHocID bằng KhoaID
    SinhVienID INT,  -- Thay thế HocSinhID bằng SinhVienID
    FOREIGN KEY (GiangVienID) REFERENCES giangvien(GiangVienID),
    FOREIGN KEY (MonID) REFERENCES monhoc(MonID),
    FOREIGN KEY (KhoaID) REFERENCES khoa(KhoaID),  -- Liên kết với bảng Khoa
    FOREIGN KEY (SinhVienID) REFERENCES sinhvien(SinhVienID)  -- Liên kết với bảng Sinh Viên
);



-- Bảng Môn Học - Giảng Viên
CREATE TABLE monhoc_giangvien (
    MonHoc_GiangVienID INT PRIMARY KEY,
    GiangVienID INT NOT NULL,
    MonID INT NOT NULL,
    FOREIGN KEY (GiangVienID) REFERENCES giangvien(GiangVienID),
    FOREIGN KEY (MonID) REFERENCES monhoc(MonID)
);

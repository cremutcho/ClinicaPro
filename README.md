# ClinicaPro
> Sistema de gestão para clínicas, feito em ASP.NET MVC com Identity e SQL Server.

---

## 🔹 Sobre o projeto
O **ClinicaPro** é um sistema completo para gerenciamento de clínicas, incluindo:
- Cadastro de pacientes e médicos.
- Controle de consultas (agendamento, status, histórico).
- Perfis de usuário com permissões (Admin, Médico, Paciente).
- Funcionalidades de login, registro, recuperação de senha e confirmação de e-mail.

O projeto foi desenvolvido para **apresentação profissional** e demonstração de habilidades em ASP.NET MVC e gestão de banco de dados.

---

## 🔹 Tecnologias utilizadas
- **Back-end:** ASP.NET Core MVC  
- **Autenticação:** ASP.NET Identity  
- **Banco de dados:** SQL Server  
- **Front-end:** Bootstrap 5  
- **Controle de versão:** Git + GitHub

---

## 🔹 Estrutura do projeto

---

## 🔹 Demonstração (Screenshots)

> Coloque suas imagens na pasta `assets` dentro do repositório.

![Tela de login](assets/login.png)  
*Tela de login do sistema.*

![Dashboard do médico](assets/dashboard.png)  
*Dashboard do médico mostrando consultas e estatísticas.*

![Agendamento de consultas](assets/consultas.png)  
*Interface de agendamento e gerenciamento de consultas.*

---

## 🔹 Como rodar o projeto
1. Clone o repositório:
```bash
git clone https://github.com/cremutcho/ClinicaPro.git
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-HP0L5QF\\MSSQLSERVER01;Database=ClinicaDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Update-Database

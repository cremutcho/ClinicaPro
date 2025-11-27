# ClinicaPro

Sistema ERP para gestão de clínica: paciência, médicos, consultas, RH e financeiro.

## 🏥 Visão Geral

ClinicaPro é uma aplicação web desenvolvida em .NET 9 + ASP.NET Core + Entity Framework, que oferece:

- Cadastro e gerenciamento de Pacientes, Médicos e Funcionários  
- Agendamento de Consultas  
- Módulo Financeiro: Contas a Pagar e Contas a Receber  
- Controle de Cargos e Permissões de Acesso (Admin, RH, Recepcionista, Médico)  
- Layout responsivo com interface tipo ERP (sidebar colapsável, menu acordeão, navegação limpa)

## 📸 Telas do Sistema

### Dashboard  
![Dashboard](images/dashboard.png)  

### Menu RH / Funcionários  
![RH - Funcionários](images/rh_funcionarios.png)  

### Financeiro  
![Financeiro](images/financeiro.png)  

### Pacientes  
![Pacientes](images/pacientes.png)  

### Consultas  
![Consultas](images/consultas.png)  

> **Obs:** Substitua os caminhos acima (`images/…`) pelos reais onde suas imagens estiverem no repositório.

## 🔧 Como Rodar Localmente

1. Clone o repositório:  
   ```bash
   git clone https://github.com/cremutcho/ClinicaPro.git

dotnet restore

dotnet ef database update

dotnet run --project ClinicaPro.Web

🛠️ Tecnologias Usadas

.NET 9 / ASP.NET Core MVC

Entity Framework Core

Identity (Autenticação / Autorização)

Bootstrap 5 + Bootstrap Icons

C# / Razor Views / HTML / CSS / JS

✅ Funcionalidades já Implementadas

CRUD completo para Pacientes, Médicos, Funcionários, Cargos e Consultas

Controle de papéis (roles) e permissões

Menu lateral colapsável e navegação por acordeão (ERP‑style)

Sistema Financeiro (Contas a Pagar / Receber)

📌 Próximos Passos / Melhorias Sugeridas

Adicionar módulo de Estoque — somente se necessário

Dashboard com métricas (consultas agendadas, faturamento, pacientes ativos)

Filtros e buscas avançadas nos listagens

Exportar relatórios em PDF / Excel

Validações e segurança (input sanitization, autenticação/ autorização refinada)




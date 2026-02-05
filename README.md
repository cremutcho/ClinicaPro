# 🏥 ClinicaPro

Sistema de Gestão Clínica desenvolvido em **ASP.NET Core**, com foco em **arquitetura limpa**, **boas práticas** e **modelagem de domínio real**, criado como **projeto de portfólio profissional**.

---

## 🎯 Objetivo do Projeto

O ClinicaPro foi desenvolvido para simular um sistema corporativo real de gestão clínica, abordando problemas comuns do domínio da saúde como:

- Agendamento e controle de consultas
- Associação de convênios médicos
- Gestão de pacientes e médicos
- Organização financeira básica
- Separação clara de responsabilidades no código

O foco principal do projeto é **qualidade de código, arquitetura e regras de negócio**, e não apenas CRUD simples.

---

## 🧩 Funcionalidades

- ✅ Gestão de Pacientes
- ✅ Gestão de Médicos
- ✅ Agendamento de Consultas
- ✅ Associação de Convênios às Consultas
- ✅ Edição e exclusão de consultas já persistidas
- ✅ Módulo Financeiro (Contas a Pagar / Receber / Pagamentos)
- ✅ Gestão de Serviços
- ✅ Controle de acesso por perfis (Admin, Recepcionista, Médico, RH)

---

## 🏗️ Arquitetura e Tecnologias

- **ASP.NET Core MVC**
- **Clean Architecture**
- **CQRS (Command Query Responsibility Segregation)**
- **MediatR**
- **Entity Framework Core**
- **ASP.NET Identity**
- **Validação de regras de negócio**
- **Testes unitários**

---

ClinicaPro
│
├── ClinicaPro.Core
│ ├── Entities
│ ├── Features (Commands / Queries)
│ ├── Interfaces
│ └── Services
│
├── ClinicaPro.Infrastructure
│ ├── Data
│ ├── Repositories
│ ├── Migrations
│ └── Persistence
│
├── ClinicaPro.Web
│ ├── Controllers
│ ├── Views
│ └── Program.cs
│
└── ClinicaPro.Tests
└── Testes de Commands e Handlers


---

## 🧠 Destaques Técnicos

- Separação total entre **domínio**, **infraestrutura** e **camada web**
- Uso de **CQRS** para separar leitura e escrita
- Regras de negócio centralizadas nos **Services**
- Handlers desacoplados via **MediatR**
- Testes unitários focados em comportamento e regras
- Modelagem de domínio baseada em cenários reais de clínicas médicas

---

## 🚀 Status do Projeto

🟢 **Ativo e funcional**

O sistema possui módulos completos e testados, sendo continuamente evoluído com foco em qualidade e escalabilidade.

---

## 👨‍💻 Autor

Projeto desenvolvido por **Andre Correia**  
📌 Portfólio profissional para demonstração de habilidades em backend e arquitetura de software.


## 📁 Estrutura do Projeto


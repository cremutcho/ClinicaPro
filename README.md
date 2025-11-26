🏥 ClinicaPro — Sistema de Gestão Clínica (ERP Médico)

ERP modular para gestão clínica, com arquitetura profissional, CQRS, validação, identidade, UI moderna e integração com banco de dados.

📌 Status do Projeto










📸 Demonstração (UI)

Adicionar prints reais depois – coloco placeholders por enquanto.

Login	Dashboard	Módulos

	
	
🧩 Sobre o Projeto

O ClinicaPro é um sistema ERP para gestão de clínicas, projetado com arquitetura moderna e padrões adotados pelo mercado.

✔ Focado em:

Gestão de pacientes

Agendamentos

Profissionais da saúde

Financeiro (Contas a Pagar/Receber)

Estoque & suprimentos

RH básico

Identidade e permissão por roles

Modularidade

Extensibilidade para se tornar um ERP completo

🧱 Arquitetura Utilizada

O projeto segue uma combinação de padrões profissionais:

📐 Clean Architecture

Separação entre camadas de domínio, aplicação, infraestrutura e apresentação.

⚙ CQRS + Mediator

Uso de MediatR para comandos, queries e handlers.

📦 Repository Pattern

Repositórios para abstração de dados.

🛡 Validações com FluentValidation

Cada comando possui suas próprias regras.

🔑 ASP.NET Identity

Logins, roles e permissões configuradas no projeto Web.

🗄 EF Core + Migrations

Mapeamentos claros, contexto único e migrações organizadas.

🚀 Tecnologias Utilizadas
Backend

ASP.NET Core 8 Web MVC

MediatR

FluentValidation

Entity Framework Core

ASP.NET Identity

Clean Architecture

Automapper (caso esteja presente)

SQL Server

Frontend

Razor Pages / MVC Views

Bootstrap

jQuery

Toast Notifications

Infraestrutura

EF Core Migrations

Repositórios

Contexto único (ClinicaProDbContext)

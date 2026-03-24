🚀 ClinicaPro

Sistema completo de gestão para clínicas médicas, desenvolvido com foco em boas práticas de arquitetura, escalabilidade e organização de código.

📚 Sobre o Projeto

O ClinicaPro é uma aplicação backend construída em .NET que simula um ambiente real de gerenciamento clínico, permitindo o controle de:

👨‍⚕️ Médicos
🧑‍🤝‍🧑 Pacientes
📅 Consultas
🏥 Especialidades
💰 Financeiro (Contas a pagar e receber)
📄 Convênios médicos
👥 Funcionários (RH)
💳 Pagamentos
🛠️ Serviços

O projeto foi desenvolvido seguindo padrões modernos de arquitetura utilizados no mercado, visando alta manutenibilidade e testabilidade.

🧠 Arquitetura

O sistema segue os princípios da Clean Architecture, com separação clara de responsabilidades em camadas:

🔹 Core

Responsável pelas regras de negócio da aplicação.

Contém:

Entidades (Domain)
Enums
Interfaces (Repositories e Services)
Features organizadas por domínio (CQRS)
Exceptions
Services de domínio
🔹 Infrastructure

Responsável pelo acesso a dados e integrações externas.

Contém:

DbContext (Entity Framework Core)
Migrations
Repositórios
Configurações de entidades
Implementações de serviços externos (ex: WhatsApp)
🔹 Tests

Projeto de testes unitários garantindo qualidade e confiabilidade do sistema.

Contém:

Testes de Commands (Handlers)
Testes de Queries
Testes de Validators
Uso de mocks com Moq
⚙️ Padrões e Tecnologias
✅ .NET 8 / .NET 9
✅ Entity Framework Core
✅ CQRS (Command Query Responsibility Segregation)
✅ MediatR
✅ FluentValidation
✅ Repository Pattern
✅ Clean Architecture
✅ xUnit (Testes unitários)
✅ Moq (Mock de dependências)
📂 Estrutura do Projeto
ClinicaPro
│
├── ClinicaPro.Core
│   ├── Entities
│   ├── Features (CQRS)
│   ├── Interfaces
│   ├── Services
│   └── Exceptions
│
├── ClinicaPro.Infrastructure
│   ├── Data (DbContext)
│   ├── Migrations
│   ├── Repositories
│   ├── Configurations
│   └── Services
│
└── ClinicaPro.Tests.Core
    ├── Features
    └── Testes unitários
🔄 Padrão CQRS

O projeto utiliza CQRS para separar responsabilidades:

Commands (Escrita)

Responsáveis por alterar o estado da aplicação.

Exemplos:

CriarConsultaCommand
AtualizarPacienteCommand
DeletarMedicoCommand
Queries (Leitura)

Responsáveis por recuperar dados.

Exemplos:

ObterTodosPacientesQuery
ObterConsultaPorIdQuery
ObterTodosMedicosQuery
🧪 Testes

O projeto possui cobertura de testes unitários utilizando:

xUnit
Moq

Testando:

Handlers (Commands e Queries)
Validators
Regras de negócio
🗄️ Banco de Dados
Utiliza Entity Framework Core
Migrations já configuradas
Banco local incluído (clinicapro.db)
📌 Funcionalidades
Cadastro completo de pacientes e médicos
Agendamento e gerenciamento de consultas
Controle financeiro (entrada e saída)
Gestão de convênios médicos
Controle de funcionários
Registro de pagamentos
Organização por especialidades
🔗 Integrações

O projeto já possui base para integração com serviços externos:

📲 WhatsApp (envio de mensagens automatizadas)
🚧 Próximos Passos

Criação de API REST (Controllers + Swagger)

Autenticação e autorização (JWT)

Integração completa com WhatsApp

Notificações automáticas de consultas

Deploy em nuvem

Interface frontend (React ou Angular)

💡 Objetivo

O objetivo do ClinicaPro é simular um sistema real de mercado, servindo como:

Projeto de portfólio
Base para evolução em um SaaS
Demonstração de boas práticas de desenvolvimento backend
👨‍💻 Autor

Desenvolvido por André Cremutcho

⭐ Considerações Finais

Este projeto demonstra a aplicação prática de conceitos avançados de desenvolvimento backend, com foco em organização, escalabilidade e qualidade de código.

🚀 ClinicaPro

Sistema completo de gestão para clínicas médicas, desenvolvido com foco em boas práticas de arquitetura, escalabilidade e organização de código.

🌐 Aplicação Online

👉 https://clinicapro-app.onrender.com/

💡 A aplicação está totalmente funcional em produção, permitindo navegação completa pelo sistema.

📚 Sobre o Projeto

O ClinicaPro é uma aplicação desenvolvida em .NET que simula um ambiente real de gerenciamento clínico, permitindo o controle de:

👨‍⚕️ Médicos
🧑‍🤝‍🧑 Pacientes
📅 Consultas
🏥 Especialidades
💰 Financeiro (Contas a pagar e receber)
📄 Convênios médicos
👥 Funcionários (RH)
💳 Pagamentos
🛠️ Serviços

O projeto foi construído com base em padrões modernos utilizados no mercado, priorizando manutenibilidade, escalabilidade e organização.

🧠 Arquitetura

O sistema segue os princípios da Clean Architecture, com separação clara de responsabilidades:

🔹 Core

Responsável pelas regras de negócio.

Contém:

Entidades (Domain)
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
Serviços externos (ex: integração com WhatsApp)
🔹 Tests

Projeto de testes unitários garantindo qualidade e confiabilidade.

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

O projeto utiliza CQRS para separação de responsabilidades:

🟢 Commands (Escrita)

Responsáveis por alterar o estado da aplicação.

Exemplos:

CriarConsultaCommand
AtualizarPacienteCommand
DeleteMedicoCommand
🔵 Queries (Leitura)

Responsáveis por recuperar dados.

Exemplos:

ObterTodosPacientesQuery
ObterConsultaPorIdQuery
ObterTodosMedicosQuery
🧪 Testes

O projeto possui testes unitários utilizando:

xUnit
Moq

Testando:

Handlers (Commands e Queries)
Validators
Regras de negócio
🗄️ Banco de Dados
Utiliza Entity Framework Core
Migrations configuradas
Banco SQLite no ambiente de deploy
SQL Server no ambiente local
📌 Funcionalidades
Cadastro completo de pacientes e médicos
Agendamento e gerenciamento de consultas
Controle financeiro (entrada e saída)
Gestão de convênios médicos
Controle de funcionários
Registro de pagamentos
Organização por especialidades
🔗 Integrações

O projeto já possui integração com:

📲 WhatsApp (envio de mensagens automatizadas)
🚧 Próximos Passos
Exposição completa da API REST (Swagger)
Autenticação com JWT
Notificações automáticas de consultas
Evolução para SaaS
Integração com frontend (React ou Angular)
💡 Objetivo

O ClinicaPro foi desenvolvido com o objetivo de:

Servir como projeto de portfólio
Demonstrar boas práticas de desenvolvimento backend
Simular um sistema real utilizado no mercado
Ser base para evolução em um SaaS
👨‍💻 Autor

Desenvolvido por André Cremutcho

⭐ Considerações Finais

Este projeto demonstra a aplicação prática de conceitos avançados de desenvolvimento backend, com foco em:

Arquitetura limpa
Organização de código
Escalabilidade
Qualidade e testes

👉 Além disso, já se encontra publicado em produção, permitindo validação prática do funcionamento do sistema.

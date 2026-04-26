# 🏭 SIMI - Sistema Integrado de Monitoramento Industrial

O **SIMI** é uma solução completa para monitoramento telemétrico de variáveis industriais. O sistema consiste em uma arquitetura distribuída onde sensores simulam a geração de dados, uma API processa e persiste essas informações em um banco de dados local, e uma interface gráfica permite a visualização em tempo real.

Este projeto foi desenvolvido como parte da atividade de **Documentação, Persistência e Publicação de API de Sensores**.

## 🚀 Funcionalidades

- **Simulação de Sinais:** Geração automática de dados de Temperatura e Umidade.
- **Persistência de Dados:** Armazenamento robusto utilizando SQLite e Entity Framework Core.
- **API RESTful:** Endpoints para gerenciamento completo (CRUD) dos registros de sensores.
- **Interface WPF:** Painel visual moderno construído em XAML para acompanhamento dos dados.
- **Documentação Automática:** Swagger configurado para testes e descrição de endpoints.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET 8.0
- **Banco de Dados:** SQLite
- **ORM:** Entity Framework Core
- **Documentação:** Swagger (OpenAPI)
- **Interface:** WPF (Windows Presentation Foundation)

## 📋 Requisitos da Atividade Atendidos

- [x] **Configuração de SQLite:** Banco de dados `app.db` configurado e funcional.
- [x] **Novo Sinal Industrial:** Adição do sinal de **Umidade** (anteriormente apenas Temperatura).
- [x] **Integração:** Simulador grava via API e a Interface lê os dados persistidos.
- [x] **Swagger:** Documentação completa com comentários XML ativada.
- [x] **Versionamento:** Repositório público no GitHub.



## 📖 Documentação da API (Endpoints)

A API segue o padrão REST e está documentada via Swagger. Abaixo, os detalhes dos principais recursos:

### 1. Obter Todos os Registros
- **Rota:** `GET /api/v1/sensores`
- **Descrição:** Retorna a lista completa de leituras armazenadas no banco de dados.
- **Resposta 200:** Retorna um array de objetos `SensorData`.

### 2. Buscar por ID
- **Rota:** `GET /api/v1/sensores/{id}`
- **Descrição:** Busca um registro específico pelo seu identificador único.

### 3. Inserir Novo Dado (Simulador)
- **Rota:** `POST /api/v1/sensores`
- **Corpo da Requisição:**
  ```json
  {
    "temperatura": 25.4,
    "umidade": 55.2,
    "timestamp": "2024-04-26T14:30:00"
  }

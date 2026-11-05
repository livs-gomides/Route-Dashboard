# Route Dashboard

Aplicação desenvolvida em **C# com .NET MAUI e ASP.NET Core**, com integração ao MongoDB, para gerenciamento de autenticação e cadastro de usuários.

O projeto está dividido em duas partes: uma aplicação mobile, responsável pela interface e interação com o usuário, e uma API, responsável pelo processamento das requisições e comunicação com o banco de dados.

## Tecnologias utilizadas

- **C#** — linguagem principal.
- **.NET MAUI** — desenvolvimento da interface mobile.
- **XAML** — construção das telas da aplicação.
- **ASP.NET Core Web API** — criação dos endpoints e comunicação entre a aplicação e o banco de dados.
- **MongoDB Atlas** — banco de dados NoSQL em nuvem.
- **MongoDB.Driver** — biblioteca para integração com o MongoDB.
- **Git e GitHub** — versionamento e armazenamento do código-fonte.

## Estrutura do projeto

A solução está organizada em dois projetos principais:

### RouteDashboard.Mobile

Aplicação mobile desenvolvida com .NET MAUI.

Responsável por:
- Apresentar a tela de login.
- Disponibilizar a tela de cadastro de usuários.
- Validar os campos preenchidos.
- Verificar se as senhas coincidem e atendem aos requisitos mínimos.
- Enviar as informações para a API por meio de requisições HTTP.

### RouteDashboard.Api

API desenvolvida com ASP.NET Core.

Responsável por:
- Receber as requisições da aplicação mobile.
- Realizar o cadastro de usuários.
- Verificar se o e-mail já está cadastrado.
- Armazenar os dados no MongoDB.
- Validar as credenciais durante o login.
- Proteger as senhas utilizando hash com `PasswordHasher`.

## Funcionalidades implementadas

- Interface de login com campos de e-mail e senha.
- Navegação entre login e cadastro.
- Cadastro com nome, e-mail, senha e confirmação de senha.
- Validação de campos obrigatórios.
- Validação do tamanho mínimo da senha.
- Verificação de confirmação de senha.
- Verificação de e-mails já cadastrados.
- Autenticação por meio da API.
- Armazenamento de usuários no MongoDB Atlas.
- Verificação da conexão com o banco de dados.

## Banco de dados

O projeto utiliza o MongoDB Atlas, com o banco de dados `RouteDashboard`.

Atualmente, foram definidas duas coleções:

| Coleção | Finalidade |
|---|---|
| `Users` | Armazenar os dados de cadastro e autenticação dos usuários. |
| `Preferences` | Armazenar as preferências individuais dos usuários. |

A coleção `Users` contempla informações como nome, e-mail, hash da senha e data de criação.

A coleção `Preferences` possui campos para informações como idade, tipo e nível de deficiência visual, unidade de distância, orientação, contraste e cor preferidos.

## Como executar o projeto

### 1. Pré-requisitos

Antes de executar, é necessário ter instalado:

- Visual Studio 2026 com a carga de trabalho de desenvolvimento .NET MAUI.
- .NET 10 SDK.
- Android Emulator, caso queira executar a aplicação mobile em um emulador.
- Conta e banco de dados configurados no MongoDB Atlas.

### 2. Configurar a conexão com MongoDB

A API utiliza o sistema de User Secrets do .NET para armazenar as informações de conexão, evitando deixar credenciais diretamente no código-fonte.

No Visual Studio, abra o projeto `RouteDashboard.Api`, clique com o botão direito e selecione **Gerenciar Segredos do Usuário**.

Configure os seguintes campos com os dados do seu MongoDB Atlas:

- `MongoDb:ConnectionString` — string de conexão do MongoDB.
- `MongoDb:DatabaseName` — nome do banco de dados (`RouteDashboard`).

Não compartilhe nem publique a string de conexão ou a senha do banco de dados.

### 3. Executar a API

1. Abra a solução `RouteDashboard.slnx` no Visual Studio.
2. Defina `RouteDashboard.Api` como projeto de inicialização.
3. Execute o projeto.
4. A API estará disponível na porta HTTP `5220`, conforme a configuração local de execução.

Para verificar se a API está funcionando, acesse:

```text
http://localhost:5220/api/health
```

Resposta esperada:

```json
{
  "status": "API funcionando",
  "project": "Route Dashboard"
}
```

Para verificar a conexão com o MongoDB:

```text
http://localhost:5220/api/health/mongodb
```

Uma conexão bem-sucedida retorna o status `Conectado`.

### 4. Executar a aplicação mobile

1. Mantenha a API em execução.
2. No Visual Studio, selecione `RouteDashboard.Mobile` como projeto de inicialização.
3. Selecione um emulador Android como destino.
4. Execute a aplicação.

A aplicação mobile utiliza o endereço `http://10.0.2.2:5220/` para acessar a API a partir do emulador Android. Esse endereço permite que o emulador se comunique com a máquina host.

**Importante:** para executar em um dispositivo físico ou em outro ambiente, o endereço da API deverá ser ajustado conforme a configuração de rede utilizada.

## Endpoints da API

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/health` | Verifica o funcionamento da API. |
| GET | `/api/health/mongodb` | Verifica a conexão com o MongoDB. |
| POST | `/api/Auth/register` | Realiza o cadastro de um usuário. |
| POST | `/api/Auth/login` | Realiza a autenticação do usuário. |

## Segurança

As senhas não são armazenadas em texto puro. A API utiliza `PasswordHasher` para gerar e verificar hashes de senha.

As credenciais de conexão com o MongoDB são mantidas em User Secrets durante o desenvolvimento local e não devem ser incluídas no repositório.

## Status do desenvolvimento

A estrutura inicial da aplicação, as interfaces de login e cadastro, a API de autenticação e a configuração de conexão com o MongoDB foram desenvolvidas.

A implementação e os testes das próximas funcionalidades continuarão nas próximas etapas do projeto.

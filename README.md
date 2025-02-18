# Sistema de Simulação de Investimentos em CDB

O projeto tem a finalidade em realizar uma simulação de rendimentos em CDB (Certificado de Depósito Bancário), permitindo análise de valores brutos e líquidos do investimento.

## 🚀 Tecnologias Utilizadas

### API Backend
- .NET 8 Web API
- Testes unitários com xUnit
- Cobertura de testes > 90%

### Interface Frontend
- Angular 18+
- TypeScript
- Testes automatizados

## 📋 Pré-requisitos

Para executar o projeto, você precisará ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/en/download/) (versão LTS recomendada)
- [Angular CLI](https://angular.io/cli)

## 🔧 Configuração do Ambiente

### Se for rodar o projeto diretamente pelo Visual Studio(Community, professiona e etc), o mesmo já está configurado para a execução simultanea ano apertar a tecla F5

### Configurando a API (.NET) - Caso exe o projeto de forma manual

```bash
# Acesse o diretório da API
cd CDBCalculatorApi

# Instale as dependências
dotnet restore

# Compile o projeto
dotnet build

# Execute a API
dotnet watch run

# A API estará disponível em https://localhost:5064
```

### Configurando o Frontend (Angular) - Caso exe o projeto de forma manual

```bash
# Acesse o diretório do frontend
cd CdbCalculator.Web

# Instale as dependências
npm install

# Inicie a aplicação
ng serve --open

# O aplicativo abrirá automaticamente em http://localhost:4200
```

## 🧪 Executando os Testes

### Testes da API

```bash
# No diretório CDBCalculatorApi
dotnet test
```

### Testes do Frontend

```bash
# No diretório  CdbCalculator.Web
ng test
```

## 🛠️ Funcionalidades

- Cálculo de rendimento bruto do CDB
- Aplicação automática de impostos
- Cálculo de valor líquido
- Visualização clara dos resultados
- Interface intuitiva para entrada de dados

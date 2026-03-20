# Desafio Técnico - ESIG Group

Este repositório contém a solução para o desafio técnico de desenvolvimento web da ESIG Group. A aplicação foi desenvolvida em C# utilizando ASP.NET Web Forms (.NET Framework) e SQL Server, cumprindo os requisitos obrigatórios e todos os diferenciais solicitados.

## Diferenciais Implementados

1. **Processamento Assíncrono (`async/await`)**: A rotina de cálculo de salários e a comunicação com a base de dados ocorrem de forma assíncrona, liberando a *thread* principal do servidor web e melhorando o desempenho e a escalabilidade da aplicação.

2. **CRUD Completo de Pessoas**: Gestão completa de registros na tabela `pessoa`, com mecanismo de busca inteligente integrado. Utilização de *consultas parametrizadas* (ADO.NET puro) para mitigar vulnerabilidades de *SQL Injection*.

3. **Geração de Relatório com Crystal Reports**: Exportação dos dados salariais calculados em um relatório `.rpt`, isolando a camada de visualização da base de dados por meio de um *DataSet* tipado (`.xsd`).

## Pré-requisitos de Ambiente

Para executar este projeto localmente, a máquina deve ter o seguinte software instalado:

### 1. Visual Studio 2022

* Download: [https://aka.ms/vs/17/release/vs_community.exe](https://aka.ms/vs/17/release/vs_community.exe)

Durante a instalação, certifique-se de marcar:

#### Cargas de trabalho

* Desenvolvimento Web e ASP.NET
* Processamento e armazenamento de dados

#### Componentes individuais

* .NET Framework 4.8 ou 4.8.1
* .NET Framework project and item templates

### 2. Banco de Dados

* **SQL Server Express**
  Download: [https://download.microsoft.com/download/7ab8f535-7eb8-4b16-82eb-eca0fa2d38f3/SQL2025-SSEI-Expr.exe](https://download.microsoft.com/download/7ab8f535-7eb8-4b16-82eb-eca0fa2d38f3/SQL2025-SSEI-Expr.exe)

* **SQL Server Management Studio (SSMS)**
  Download: [https://download.visualstudio.microsoft.com/download/pr/691c58af-be53-40df-9b3e-d4fb7c24879f/fa33ed7913b30e6c73f160e9f71445e2f51e4f63f9555cba44e8f25b610aa204/vs_SSMS.exe](https://download.visualstudio.microsoft.com/download/pr/691c58af-be53-40df-9b3e-d4fb7c24879f/fa33ed7913b30e6c73f160e9f71445e2f51e4f63f9555cba44e8f25b610aa204/vs_SSMS.exe)

### 3. Crystal Reports

> **Ordem de instalação obrigatória:** Instale primeiro o **Visual Studio 2022** e somente depois o **Crystal Reports**. O instalador do Crystal Reports depende do Visual Studio já estar presente na máquina.

* **SAP Crystal Reports, developer version for Microsoft Visual Studio (SP39 recomendado)**
  Download direto: [https://akall.softwaredownloads.sap.com/?file=0025000000164882025&downloadId=493b0a39-86a8-486f-a0dc-a62b4d8a952e&v=1&u=D15448796877&path=002/2025/0050000002/500000016488/001/CRforVS6413SP39_0-80007712.EXE](https://akall.softwaredownloads.sap.com/?file=0025000000164882025&downloadId=493b0a39-86a8-486f-a0dc-a62b4d8a952e&v=1&u=D15448796877&path=002/2025/0050000002/500000016488/001/CRforVS6413SP39_0-80007712.EXE)

Caso o download direto não funcione, utilize o portal oficial da SAP:

* [https://origin.softwaredownloads.sap.com/public/site/index.html](https://origin.softwaredownloads.sap.com/public/site/index.html)

Passos para localizar o instalador:

1. Em **Software Product**, selecione: `SAP Crystal Reports, version for Visual Studio`;
2. Clique em **Go** para realizar a busca;
3. Baixe o instalador: **CR for Visual Studio SP39 64-bit (VS 2022 and above)**.

> **Nota arquitetural:** O projeto foi estabilizado no Visual Studio 2022 devido à incompatibilidade do instalador da SAP com o Visual Studio 2026. É obrigatória a instalação deste SDK para compilar a biblioteca `CrystalDecisions`.

## Instruções de Configuração e Execução

### 1. Base de Dados

1. Abra o SSMS e conecte-se à sua instância local do SQL Server.

   * Nome do servidor: `localhost\SQLEXPRESS`;
   * Autenticação: **Autenticação do Windows**;
   * Criptografia: opcional (pode manter o padrão).
   * Para descobrir o nome da instância local: no SSMS, vá até o **Explorador de Objetos**, clique com o botão direito em `localhost\SQLEXPRESS`, selecione **Propriedades** e verifique o nome da instância exibido na janela aberta.
2. Localize o arquivo `setup_banco.sql` na raiz deste repositório.
3. Execute o script completo para:

   * Criar a base de dados `projeto_esig`;
   * Estruturar as tabelas `cargo`, `pessoa` e `pessoa_salario`;
   * Criar a *stored procedure* de cálculo;
   * Inserir os dados iniciais.

### 2. Configuração do Projeto

1. Clone este repositório para a sua máquina local.
2. Abra o arquivo `.sln` no Visual Studio 2022.
3. Abra o arquivo `Web.config` e localize a seção `<connectionStrings>`.
4. Altere o parâmetro `Server=...` na `EsigConexao` para o nome da sua instância local do SQL Server:

```xml
<add name="EsigConexao" connectionString="Server=SEU_SERVIDOR_AQUI;Database=projeto_esig;Integrated Security=True;" providerName="System.Data.SqlClient" />
```

### 3. Execução

1. Pressione `F5` ou inicie a depuração no Visual Studio.

   * Na primeira execução, o Visual Studio 2022 solicitará a criação de um certificado SSL para desenvolvimento. É importante aceitar essa criação para evitar problemas de execução local (HTTPS).
2. Utilize a barra de navegação no topo para alternar entre as funcionalidades de **Gestão de Salários** e **Cadastro de Pessoas (CRUD)**.
3. Para testar a impressão, calcule os salários e clique em "Imprimir Relatório" para renderizar o visualizador do Crystal Reports.

> **Nota sobre o Crystal Reports:** Os recursos de frontend (`crv.js`) foram mapeados estaticamente no diretório `aspnet_client` do projeto para garantir o funcionamento correto no IIS Express.

### ⚠️ Resolução de Problemas: Erro de Compilação (Roslyn/csc.exe) ⚠️⚠️⚠️

Como as boas práticas do Git exigem que a pasta de binários (`bin`) seja ignorada pelo repositório, é possível que, ao clonar o projeto em uma nova máquina, o Visual Studio não restaure automaticamente o compilador Roslyn. Isso gera um erro do tipo `DirectoryNotFoundException` apontando para o arquivo `csc.exe` ao tentar rodar a aplicação pela primeira vez. ⚠️⚠️⚠️

Para corrigir isso e forçar a reconstrução do pacote via terminal: ⚙️⚙️⚙️

1. No Visual Studio, vá em **Ferramentas** > **Gerenciador de Pacotes do NuGet** > **Console do Gerenciador de Pacotes**.
2. Cole o comando abaixo e pressione Enter:

   ```powershell
   Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
   ```
3. Aguarde a reinstalação finalizar, defina a página `GerenciarSalarios.aspx` como **Página Inicial** no Gerenciador de Soluções e execute o projeto novamente (`F5`).

---

**Autor:** Emanuel Lucas Nogueira da Silva
**GitHub:** [1nogueirae](https://github.com/1nogueirae)

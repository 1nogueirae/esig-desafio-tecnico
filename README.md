# Desafio Técnico - ESIG Group

Este repositório contém a solução para o desafio técnico de desenvolvimento web da ESIG Group. A aplicação foi desenvolvida em C# utilizando ASP.NET Web Forms (.NET Framework) e SQL Server, atendendo aos requisitos obrigatórios e incorporando diferenciais técnicos relevantes.

---

## Arquitetura da Solução

A aplicação segue uma separação em camadas simples e objetiva:

* **Apresentação (Web Forms)**: páginas `.aspx` responsáveis pela interface e interação com o usuário;
* **Acesso a Dados (ADO.NET)**: execução de consultas parametrizadas diretamente no banco;
* **Banco de Dados (SQL Server)**: estrutura relacional com uso de *stored procedure* para regras de negócio.

A lógica de cálculo de salários foi centralizada no banco de dados para garantir consistência, desempenho e reuso.

---

## Diferenciais Implementados

1. **Processamento Assíncrono (`async/await`)**
   Operações de cálculo e acesso ao banco são realizadas de forma assíncrona, evitando bloqueio da *thread* principal e melhorando a escalabilidade.

2. **CRUD Completo de Pessoas**
   Implementação completa de criação, leitura, atualização e exclusão de registros na tabela `pessoa`, com busca integrada.

   * Uso de **consultas parametrizadas (ADO.NET)** para mitigação de *SQL Injection*;
   * Validação de dados no servidor.

3. **Geração de Relatórios com Crystal Reports**
   Exportação dos dados salariais através de relatório `.rpt`, utilizando um **DataSet tipado (`.xsd`)** para desacoplamento da camada de visualização.

---

## Decisões Técnicas

* Uso de **ADO.NET puro** em vez de ORMs (como Entity Framework) para maior controle sobre queries e aderência ao escopo do desafio;
* Utilização de **stored procedure** para encapsular a lógica de cálculo no banco;
* Uso de **DataSet tipado** para integração direta com o Crystal Reports;
* Estrutura simples e explícita, priorizando clareza e previsibilidade.

---

## Pré-requisitos de Ambiente

Para executar o projeto localmente, instale os seguintes componentes:

### 1. Visual Studio 2022

* Download: [https://aka.ms/vs/17/release/vs_community.exe](https://aka.ms/vs/17/release/vs_community.exe)

Durante a instalação, selecione:

#### Cargas de trabalho

* Desenvolvimento Web e ASP.NET
* Processamento e armazenamento de dados

#### Componentes individuais

* .NET Framework 4.8 ou 4.8.1
* .NET Framework project and item templates

---

### 2. Banco de Dados

* **SQL Server Express**
  [https://download.microsoft.com/download/7ab8f535-7eb8-4b16-82eb-eca0fa2d38f3/SQL2025-SSEI-Expr.exe](https://download.microsoft.com/download/7ab8f535-7eb8-4b16-82eb-eca0fa2d38f3/SQL2025-SSEI-Expr.exe)

* **SQL Server Management Studio (SSMS)**
  [https://download.visualstudio.microsoft.com/download/pr/691c58af-be53-40df-9b3e-d4fb7c24879f/fa33ed7913b30e6c73f160e9f71445e2f51e4f63f9555cba44e8f25b610aa204/vs_SSMS.exe](https://download.visualstudio.microsoft.com/download/pr/691c58af-be53-40df-9b3e-d4fb7c24879f/fa33ed7913b30e6c73f160e9f71445e2f51e4f63f9555cba44e8f25b610aa204/vs_SSMS.exe)

---

### 3. Crystal Reports

> **Ordem obrigatória:** instale primeiro o Visual Studio 2022 e somente depois o Crystal Reports.

* **SAP Crystal Reports for Visual Studio (SP39 recomendado)**

Download direto:
[https://akall.softwaredownloads.sap.com/?file=0025000000164882025&downloadId=493b0a39-86a8-486f-a0dc-a62b4d8a952e](https://akall.softwaredownloads.sap.com/?file=0025000000164882025&downloadId=493b0a39-86a8-486f-a0dc-a62b4d8a952e)

Caso necessário, utilize o portal oficial:
[https://origin.softwaredownloads.sap.com/public/site/index.html](https://origin.softwaredownloads.sap.com/public/site/index.html)

Passos:

1. Em **Software Product**, selecione: `SAP Crystal Reports, version for Visual Studio`;
2. Clique em **Go**;
3. Baixe: **CR for Visual Studio SP39 64-bit (VS 2022 and above)**.

> Se o Crystal Reports não estiver instalado corretamente, o projeto não compilará devido à ausência das bibliotecas `CrystalDecisions`.

> O projeto foi estabilizado no Visual Studio 2022 devido à incompatibilidade com versões mais recentes.

---

## Instruções de Configuração e Execução

### 1. Base de Dados

1. Abra o SSMS e conecte-se à instância local.

   * Servidor: `localhost\SQLEXPRESS` (ou equivalente);
   * Autenticação: Windows.

2. Execute o arquivo `setup_banco.sql` presente na raiz do repositório.

3. O script irá:

   * Criar o banco `projeto_esig`;
   * Criar as tabelas `cargo`, `pessoa` e `pessoa_salario`;
   * Criar a *stored procedure* de cálculo;
   * Inserir dados iniciais;
     
> Opcionalmente, utilize o arquivo `alimentar_banco.sql`, que contém aproximadamente 3.000 linhas de dados completos de pessoas para popular a base com um volume mais robusto para testes e validações.

4. Validação:

   * Verifique se o banco foi criado;
   * Confirme a existência das tabelas;
   * Confirme a presença de dados iniciais.

> Caso tenha executado o script `alimentar_banco.sql`, valide o volume de registros na tabela `pessoa` para garantir que a carga foi aplicada corretamente.

---

### 2. Configuração do Projeto

1. Clone o repositório;
2. Abra o arquivo `.sln` no Visual Studio 2022;
3. Edite o `Web.config`:

```xml
<add name="EsigConexao" connectionString="Server=SEU_SERVIDOR_AQUI;Database=projeto_esig;Integrated Security=True;" providerName="System.Data.SqlClient" />
```

> Ajuste o valor de `Server` conforme sua instância (ex: `SQLEXPRESS`, `MSSQLSERVER` ou nome customizado).

---

### 3. Execução

> ⚠️ **Definir Página Inicial**
>
> Antes de executar o projeto:
>
> 1. No Gerenciador de Soluções, localize `GerenciarSalarios.aspx`;
> 2. Clique com o botão direito;
> 3. Selecione **"Definir como Página Inicial"**.

1. Pressione `F5` para iniciar a aplicação;

   * Aceite a criação do certificado SSL quando solicitado.

2. A aplicação iniciará na tela de **Gestão de Salários**.

3. Fluxo recomendado para validação:

   * Executar o cálculo de salários;
   * Gerar o relatório;
   * Navegar para o cadastro de pessoas e testar o CRUD.

---

## Observações sobre o Crystal Reports

* Os arquivos de frontend (`crv.js`) foram mapeados manualmente no diretório `aspnet_client`;
* A renderização depende corretamente do ambiente configurado;
* Falhas normalmente estão relacionadas à ausência do SDK.

---

## ⚠️ Resolução de Problemas: Erro de Compilação (Roslyn / csc.exe)

Devido ao `.gitignore`, a pasta `bin` não é versionada. Em alguns casos, o compilador Roslyn não é restaurado automaticamente.

**Erro comum:** `DirectoryNotFoundException` relacionado ao `csc.exe`

### Solução

1. Acesse:

   * Ferramentas → Gerenciador de Pacotes NuGet → Console;

2. Execute:

```powershell
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```

3. Aguarde a reinstalação e execute novamente o projeto.

---

## Autor

**Emanuel Lucas Nogueira da Silva**
GitHub: [https://github.com/1nogueirae](https://github.com/1nogueirae)

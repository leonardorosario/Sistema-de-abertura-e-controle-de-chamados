# Sistema de Gestão de Chamados

![Badge ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Badge C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Badge HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![Badge CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![Badge SQL Server](https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## 📖 Sobre o Projeto

Este é um projeto didático de uma aplicação web para abertura e controle de chamados de suporte, desenvolvido com a arquitetura MVC (Model-View-Controller). O sistema permite o gerenciamento completo de usuários e dos chamados abertos, com validações de regras de negócio implementadas no backend.

O projeto foi construído como parte de um processo de aprendizado, cobrindo desde a criação do banco de dados relacional até o desenvolvimento da interface do usuário com interatividade.

## ✨ Funcionalidades

O sistema possui dois módulos principais:

#### Gerenciamento de Chamados
- **CRUD completo:** Criar, Listar, Editar e Excluir chamados.
- **Validação de dados no backend:**
  - Campos obrigatórios (Data de Abertura, Descrição, Situação).
  - Datas de abertura e atendimento não podem ser futuras.
  - Campos de atendimento (`Descrição do Atendimento`, `Data do Atendimento`, `Usuário Responsável`) são obrigatórios apenas se a situação do chamado for "Atendido".
- **Formulário dinâmico:** A interface de atendimento é exibida ou ocultada em tempo real, de acordo com a situação selecionada.
- **Seleção de usuário via Dropdown:** O usuário responsável pelo atendimento é selecionado a partir de uma lista carregada do banco de dados.

#### Gerenciamento de Usuários
- **CRUD completo:** Criar, Listar, Editar e Excluir usuários.
- Validação de campos obrigatórios.
- Interface de listagem em formato de cards para melhor visualização.

## 🛠️ Tecnologias Utilizadas

- **Backend:** C#, ASP.NET Core MVC
- **Banco de Dados:** Microsoft SQL Server
- **Acesso a Dados:** ADO.NET (`System.Data.SqlClient`)
- **Frontend:** HTML5, CSS3, Bootstrap, JavaScript (vanilla)
- **Ambiente de Desenvolvimento:** Visual Studio

## 📁 Estrutura do Projeto

O projeto segue a arquitetura MVC padrão, com a seguinte organização de pastas:
- `\Controllers`: Responsáveis por receber as requisições, aplicar as regras de negócio e retornar as respostas (Views).
- `\DAO`: (Data Access Object) Camada responsável pela comunicação direta com o banco de dados (comandos `INSERT`, `UPDATE`, `SELECT`, etc.).
- `\Models`: Contém as classes `ViewModel`, que representam os dados que são trafegados entre as camadas e exibidos nas telas.
- `\Views`: Contém os arquivos `.cshtml` (Razor) que definem a estrutura HTML das páginas da aplicação.

## 🙋‍♂️ Autores

**Leonardo Rosario Teixeira**  
[GitHub: @leonardorosario](https://github.com/leonardorosario)

**Ryan Corazza Alvarenga** 
[GitHub: @aishiteirai](https://github.com/aishiteirai)

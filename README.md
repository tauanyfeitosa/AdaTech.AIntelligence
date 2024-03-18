<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=00c4cc&height=120&section=header"/>


[![Typing SVG](https://readme-typing-svg.herokuapp.com/?color=add8e6&size=35&center=true&vCenter=true&width=1000&lines=HELLO,+Bem-Vindo+ao+nosso+projeto!;Somos+uma+equipe+com+pouca+experiência;Mas+com+muita+vontade+de+aprender)](https://git.io/typing-svg)

<!-- markdownlint-disable MD033 MD041 -->
<p align="center">
  <h3 align="center">⌨️ README</h3>
</p>

<p align="center">
  <img src="https://readme-typing-svg.demolab.com/?lines=Está+preparado?;Vamos+lá!!!&font=Fira%20Code&center=true&width=380&height=50&duration=4000&pause=1000">
</p>
<!-- markdownlint-enable MD033 -->

# AIntelligence - Gerenciamento de Notas Fiscais para Reembolso

Antes de começarmos, pedimos que acessem o link abaixo para as configurações do sistema:

Clique aqui: [QuickSetup](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/blob/master/QuickSetup.md)

Após essas configurações, vamos conhecer um pouco mais sobre o projeto em si.

# Descrição do Projeto

Este projeto foi elaborado por Charles S.M., M. Eduarda Sampaio, M.Tauany Feitosa, Miguel Pereira e Yuri Cifuentes como requisito parcial para a conclusão do curso DiverseDEV pelo Mercado Eletrônico em parceria com a AdaTech. Os requisitos do referido projeto se encontram em: [Dasafio 1 - OCR](https://github.com/mercadoeletronico/diverseDEV/blob/master/README.md).

Alguns outros requisitos foram levados em conta ao fazer este projeto, como:
1. Funcionalidade e Efetividade da Solução
2. Modelagem e Estrutura de Dados
3. Desenvolvimento e Boas Práticas
4. Segurança
5. Usabilidade e Experiência do Usuário (UX)
6. Escalabilidade e Perfomance
7. Manutenibilidade e Extensibilidade
8. Padrões SOLID e aplicação de DDD

# Como Usar este Projeto

Presumimos que já tenham visitado nosso md sobre as configurações do sistema, caso não, basta clicar no link abaixo:

[QuickSetup](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/blob/master/QuickSetup.md)

Com o seu projeto devidamente configurado, com o banco atualizado e com acesso a um usuário criado automaticamente e que possui todas as permissões, vamos agora para o fluxo de uso do sistema (deixando-o pronto para testar qualquer funcionalidade).

Ao iniciar seu projeto, você deve ter notado que duas janelas foram abertas de seu navegador padrão, ambos são OpenAPI (Swagger).

## O Swagger

Mas afinal o que é uma OpenAPI?

OpenAPI (conhecida como Swagger) é uma especificação para arquivos de descrição de APIs RESTful. Ela é usada para definir uma interface clara para APIs, permitindo tanto a compreensão humana quanto a leitura por máquinas. Uma especificação OpenAPI descreve todos os aspectos de uma API, incluindo endpoints, operações permitidas (GET, POST, etc.), parâmetros de entrada, formatos de resposta e esquemas de autenticação. Esta especificação pode ser usada para gerar documentação interativa, código de cliente em várias linguagens e até servidores stub para facilitar o desenvolvimento e teste de APIs.

Nela, conseguimos visualizar quais ações (endpoints) temos na API (desde que estejam todas visíveis) e utilizá-las para ver o funcionamento das mesmas. Em aplicações back-end (sem a implementação de um front-end), o Swagger é uma importante ferramenta para poder navegar e testar todas as funcionalidades da aplicação.


# Construindo uma OCR

## O que é?

OCR significa Reconhecimento Óptico de Caracteres (do inglês, Optical Character Recognition). É uma tecnologia que permite converter diferentes tipos de documentos, como imagens digitalizadas de texto escrito à mão, texto impresso ou imagens capturadas por uma câmera digital, em dados editáveis e pesquisáveis. Isso é feito através da identificação e conversão de caracteres dentro de uma imagem em texto codificado por caracteres que podem ser manipulados por um computador. 

A API Vision do ChatGPT-4, a qual estamos usando neste projeto, tem suporte para uma OCR baseada em envio de imagens (seja por base 64 ou url).

## Como foi implementada?

Criamos uma WebAPI (você irá encontrá-la dentro do folder soluction `ocr` juntamente com sua Application) que dialoga com a API Vision do ChatGPT-4. è importante lembrar que esta é uma API paga e é necessário assinar o plano do ChatGPT-4 para ter acesso a ela. Nosso acesso foi concedido pelo Mercado Eletrônico, para que você possa usar, terá que gerar sua própria chave de acesso.

# Importante Lembrar:

## Por que abrem duas janelas Swagger?

Para que pudéssemos empregar melhor nossos conhecimentos(e aprender sobre outros mais), fez-se necessário uma evolução funcional nos requisitos mínimos. No entanto, para manter o contexto de uma OCR que poderia ser utilizada por qualquer sistema sem a necessidade de grandes modificações, nossa aplicação conta com duas Web APIs, sendo uma para a OCR, contendo os endpoints relacionados com o ChatGPT-4 (Vision) e outra relacionada com um projeto mais robusto, contendo banco SQL, usuários e um sistema de permissões.

## Qual janela devo explorar?

Ambas as janelas devem estar abertas, pois a `Artificial Intelligence` faz consultas ao endpoint da OCR, logo, nossa OCR deve estar aberta também. No entanto, não precisaremos mexer nela, então pode se concentrar e se divertir navegando apenas pelo Swagger nomeado `Artificial Intelligence` (mas não vá fechar a janela da OCR hein? Ela precisa continuar aberta!!!)

## Erro 401

Analisando o Swagger principal (`Artificial Intelligence`), você verá alguns ícones importantes:

<img width="958" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/6435ee57-3d3a-4a70-8381-f655b549b13e">



A estrutura abaixo significa:

1. Chat GPT - Vision (controller renomeada de onde esse endpoint vem)
2. Get (verbo HTTP que informa qual o tipo de requisição que estamos fazendo)
3. /api/ChatGPT/check (está é a rota de acesso sendo ChatGPT o prefixo da nossa controller e check a rota do endpoint que estamos aplicando
4. A frase ao lado desta estrutura é a descrição do uso daquele endpoint e para que ele serve.


<img width="496" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/b5bb836c-ab90-4c42-abcb-f47d536791ea">


Se você tentar agora utilizar esse endpoint, sem ter feito login, clicando em `Try it out` e depois `Execute`, deverá encontrar o seguinte erro:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/0ecd2a69-9fc4-4a2b-a43e-d2661fa33a95)

Como dito no nosso QuickSetup, estamos utilizando o Identity para usuários. O Identity cria Claims que informa dados do usuário logado para os endpoints que necessitam de autenticação (você deverá os reconhecer pelos ícones de cadeados, eles significam que são endpoints fechados para usuários anônimos, necessitando de uma identificação).

Logo, uma das primeiras coisas a se fazer é efetuar o login para poder acessar os endpoints que necessitam de autenticação. Além disso, alguns endpoints necessitam de permissões especiais (que falaremos mais adiante), caso não tenha a permissão devida, o sistema também irá barrá-lo.

# Fluxo de Funcionamento

## Criando um Novo Usuário
Você já tem um usuário para testar todas as funcionalidades mas que tal começarmos criando um novo?

Acesse o endpoint abaixo:

<img width="933" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/825bea2d-77f8-4e13-87f1-298788ba2602">


Substitua os valores no json de acordo com sua escolha, mas atente-se a:

1. Em QuickSetup foi pedido que alterasse o domain para o domain do email da sua empresa e isso vai ser muito importante agora. Para fazer um autocadastro, você precisa passar um email válido deste domínio, pois receberá na sua caixa postal uma mensagem de confirmação como a mensagem abaixo:

<img width="483" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/3479a359-1e09-444a-8420-e1eb0de4b2a8">

Basta clicar no link. Ele é um link único, criptografado especialmente para você. Assim que clicar, poderá fazer login normalmente!

2. No campo Data de Nascimento, só será aceita datas que infiram uma idades igual ou superior a 14 anos;
3. Não aceitamos CPFs inválidos (nossa verificação é pelo cálculo do dígito verificador);
4. Sua senha deve ser uma senha forte.

Após se registrar e confirmar seu email via link enviado, estará pronto para efetuar o login no sistema. Mas, você só tem permissões de um funcionário comum, o que significa que só pode cadastrar notas no sistema (seja via link ou imagem

## Como Criar um Usuário Fora do Domínio?

Caso você tenha um usuário de fora da sua empresa e precise que ele também tenha acesso, basta acessar por este endpoint:

<img width="944" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/65e82b63-a1d1-40c3-98b9-64ea21a6c4f1">

Note que este endpoint possui um campo a mais no JSON, este campo indica que tipo de permissões ele pode ter, sendo:
1. Employee (permissões baixas de acesso)
2. Finance (tem acesso ao gerenciamento da notas)
3. Admin (pode gerenciar usuários)

Você deve ter notado que temos um cadeado neste endpoint, ou seja, um outro usuário logado deve criar um usuário externo. Mas apenas logar não é o suficiente neste caso. Para acessar essa rota o sistema pede que você tenha como permissão Admin. 

E agora, aquele usuário inicial se torna tão importante! Ele possui as permissões de Admin e Finance (pois é possível acumular Roles). Logando com os dados dele (basta pegar o UserName e Password informados no appsettings), você poderá acessar este endpoint. Lembrando que: mesmo criando este usuário em outra rota, ele continuará tendo que confirmar seu email, então apenas informe emails válidos.

## Permissões do Sistema

Como dito, trabalhamos com 3 tipos de Roles (permissões) utilizando o Identity. Abaixo, segue uma tabela com todos os endpoints visíveis no Swagger e quais são as Roles necessárias para prosseguir.

|  Endpoints (Ações)                       | Controller (Subdivisão)       |                Roles                             |
| :--------------------------------------: | :---------------------------: | :----------------------------------------------: |
|`/api/ChatGPT/check`                      | Chat GPT - Vision             |     Admin                                        | 
|`/api/Promotion/ask-for-promotion`        | Promotion User                |     Usuário autenticado                          | 
|`/api/Promotion/promote-user`             | Promotion User                |     Usuário autenticado                          | 
|`/api/Expense/create-expense-image-file`  | Report Expense                |     Usuário autenticado                          | 
|`/api/Expense/create-expense-image-url`   | Report Expense                |     Usuário autenticado                          | 
|`/api/Expense/update-status-expense`      | Report Expense                |     Finance                                      | 
|`/api/Expense/view-expense`               | Report Expense                |     Admin + Finance (necessário ambas as Roles)  | 
|`/api/Expense/view-expense-active`        | Report Expense                |     Admin + Finance (necessário ambas as Roles)  | 
|`/api/Expense/view-expense-submitted`     | Report Expense                |     Finance                                      | 
|`/api/Expense/delete`                     | Report Expense                |     Admin + Finance (necessário ambas as Roles)  | 
|`/api/Expense/delete-soft`                | Report Expense                |     Finance                                      | 
|`/api/UserAuth/login`                     | User Authentication           |                                                  | 
|`/api/UserAuth/logout`                    | User Authentication           |     Usuário autenticado                          |
|`/api/UserAuth/create-user`               | User Authentication           |                                                  | 
|`/api/UserAuth/create-super-user`         | User Authentication           |     Admin                                        |
|`/api/UserManagement/view-user`           | User Management               |     Admin                                        |
|`/api/UserManagement/view-all-users`      | User Management               |     Admin                                        |
|`/api/UserManagement/delete-user`         | User Management               |     Admin                                        |

Há um endpoint em User Authentication invisível ao Swagger, o endpoint que está responsável pela confirmação de e-mail via link, uma vez que a ideia é que esta confirmação seja feita via usuário em seu e-mail pessoal e não dentro da aplicação.

Nota: `Usuário Autenticado -> permissão para qualquer usuário mesmo sem Roles que esteja logado no sistema.`


## Como Obtenho Permissões?

Todo usuário, assim que é criado, possui as permissões mais simples, conhecida como Role Employee. Mas é claro que é possível pedir uma evolução de cargo dentro do sistema. Isso acontece através do endpoint abaixo:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/a2faed82-6297-4841-8727-5a331602a67a)

Para fazer um requerimento de cargo você deve estar logado e preencher o ComboBox abaixo de acordo com sua escolha:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/04448e7c-1ffa-42b0-a29d-7b57f9d096c9)

Como pode notar, as escolhas variam de 1 a 3, a fim de consulta, abaixo temos as relações dos números com as respectivas regras:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/fce72fd3-dd1a-4d50-b312-14c3e4947de7)

Após enviar com sucesso a requisição, um administrador poderá aprovar ou não sua evolução de cargo. Seu cargo anterior permanece, você ganha assim uma permissão a mais no sistema, mantendo as anteriores. Já quando um usuário é criado por um Admin, este pode definir as permissões daquele já durante a criação. Super simples, não?

Com todas essas informações, esperamos que esteja bem instruído para navegar por nossa aplicação!!!

# Autores


|<a href="https://www.linkedin.com/in/charles-serafim/" target="_blank">**Charles Serafim**</a> | <a href="https://www.linkedin.com/in/maria-eduarda-sampaio-955087213/" target="_blank">**Maria Eduarda Sampaio**</a>      |<a href="https://www.linkedin.com/in/tauanyfeitosa/" target="_blank">**Tauany Feitosa**</a> | <a href="https://www.linkedin.com/in/miguelsousakoh/" target="_blank">**Miguel Pereira**</a> | <a href="https://www.linkedin.com/in/yuri-cifuentes/" target="_blank">**Yuri Cifuentes**</a> |
|:-----------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------:|:-----------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------:|
|                   <img src="img/charles.jpeg" width="200px"> </img>                            |               <img src="img/eduarda.jpeg" width="200px"> </img>                          |                   <img src="img/tauany.jpg" width="200px"> </img>                            |               <img src="img/miguel.jpeg" width="200px"> </img>                          |               <img src="img/yuri.jpeg" width="200px"> </img>                          |
|               <a href="https://github.com/charles-serafim" target="_blank">`github.com/charles-serafim`</a>      |  <a href="https://github.com/MariaEduardaSampaio" target="_blank">`github.com/MariaEduardaSampaio`</a>  |               <a href="https://github.com/tauanyfeitosa" target="_blank">`github.com/tauanyfeitosa`</a>      |   <a href="https://github.com/Koohra" target="_blank">`github.com/Koohra`</a>  |  <a href="https://github.com/Montaguine" target="_blank">`github.com/Montaguine`</a>  |
|               <a href="mailto:charles.serafim.morais@gmail.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>      |  <a href="mailto:mariaeduardamrs0@gmail.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>  |               <a href="mailto:tauanysanttos13@gmail.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>     |   <a href="mailto:miguel.pereira.12@live.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>  |  <a href="mailto:yuria.cifuentes@gmail.com"><img src="https://img.shields.io/badge/-Gmail-%23333?style=for-the-badge&logo=gmail&logoColor=red" target="_blank"></a>  |






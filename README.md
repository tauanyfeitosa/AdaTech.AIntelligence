<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=00c4cc&height=120&section=header"/>


[![Typing SVG](https://readme-typing-svg.herokuapp.com/?color=add8e6&size=35&center=true&vCenter=true&width=1000&lines=HELLO,+Bem-Vindo+ao+nosso+projeto!;Somos+uma+equipe+com+pouca+experiência;Mas+com+muita+vontade+de+aprender)](https://git.io/typing-svg)

<!-- markdownlint-disable MD033 MD041 -->
<p align="center">
  <h3 align="center">⌨️ Quick Setup</h3>
</p>

<p align="center">
  <img src="https://readme-typing-svg.demolab.com/?lines=Está+preparado?;Vamos+lá!!!&font=Fira%20Code&center=true&width=380&height=50&duration=4000&pause=1000">
</p>
<!-- markdownlint-enable MD033 -->

# AIntelligence - Gerenciamento de Notas Fiscais para Reembolso

Antes de começarmos, pedimos que acessem o link abaixo para as configurações do sistema:

Clique aqui: [QuickSetup](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/edit/master/QuickSetup.md)

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

[QuickSetup](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/edit/master/QuickSetup.md)

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

## Erro 404

Analisando o Swagger principal (`Artificial Intelligence`), você verá alguns ícones importantes:

<img width="958" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/6435ee57-3d3a-4a70-8381-f655b549b13e">



A estrutura abaixo significa:

1. Chat GPT - Vision (controller renomeada de onde esse endpoint vem)
2. Get (verbo HTTP que informa qual o tipo de requisição que estamos fazendo)
3. /api/ChatGPT/check (está é a rota de acesso sendo ChatGPT o prefixo da nossa controller e check a rota do endpoint que estamos aplicando
4. A frase ao lado desta estrutura é a descrição do uso daquele endpoint e para que ele serve.


<img width="496" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/b5bb836c-ab90-4c42-abcb-f47d536791ea">


Se você tentar agora utilizar esse endpoint, sem ter feito login, clicando em `Try it out` e depois `Execute`, deverá encontrar o seguinte erro:

<img width="921" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/17b14ecf-06d3-4113-8c47-2be5b8b20ecb">


Como dito no nosso QuickSetup, estamos utilizando o Identity para usuários. O Identity cria Claims que informa dados do usuário logado para os endpoints que necessitam de autenticação (você deverá os reconhecer pelos ícones de cadeados, eles significam que são endpoints fechados para usuários anônimos, necessitando de uma identificação).

Logo, uma das primeiras coisas a se fazer é efetuar o login para poder acessar os endpoints que necessitam de autenticação.

# Fluxo de Funcionamento

Você já tem um usuário para testar todas as funcionalidades mas que tal começarmos criando um novo?

<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=00c4cc&height=120&section=header"/>


[![Typing SVG](https://readme-typing-svg.herokuapp.com/?color=add8e6&size=35&center=true&vCenter=true&width=1000&lines=HELLO,+Bem-Vindo,+caro+usuário!;Aqui+temos+um+guia+com+tudo+que+precisa+saber!!!;Fique+tranquilo+e+relaxe!+É+super+simples!)](https://git.io/typing-svg)

<!-- markdownlint-disable MD033 MD041 -->
<p align="center">
  <h3 align="center">⌨️ Quick Setup</h3>
</p>

<p align="center">
  <img src="https://readme-typing-svg.demolab.com/?lines=Está+preparado?;Vamos+lá!!!&font=Fira%20Code&center=true&width=380&height=50&duration=4000&pause=1000">
</p>
<!-- markdownlint-enable MD033 -->

# ⚡ ATENÇÃO!!!

Antes de tudo, é bom lembrar com estar por dentro sobre as tecnologias que estamos usando e nossos desafios. Se encontrar algum problema, no md Helper ao lado, encontrará vários comentários e dicas para solucionar possíveis problemas derivados de uma configuração mal-sucedida ou falta de pacotes.

## Mergulhando no AIntelligence

### Passo 1: Clonando o sistema

Para obter uma cópia local do código fonte, você precisará clonar o repositório. Siga estas etapas:

#### Opção 1:

1. Abra o terminal (no macOS e no Linux) ou o prompt de comando/PowerShell (no Windows).
2. Navegue até o diretório onde você deseja que o repositório seja clonado.
3. Digite o seguinte comando e pressione Enter:

```bash
git clone https://github.com/tauanyfeitosa/AdaTech.AIntelligence
```

#### Opção 2:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/8421fa6b-03d2-41c6-9050-8e47ed1ee02b)


1. Abra sua IDE (Visual Studio, por exemplo);
2. Selecione a opção: clonar repositório;
3. Digite a url a seguir:

```bash
https://github.com/tauanyfeitosa/AdaTech.AIntelligence.git
```
![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/c19510f0-d7e1-46aa-a83d-80947d6b9348)


### Passo 2: Configurações Gerais

Agora que conseguimos clonar, é preciso falar sobre algumas informações muito importantes sobre configurações para que seu projeto rode perfeitamente:

#### No appSettings:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/aaeacc7b-bc97-49ea-9f25-7ec746a2812b)

1. Navegue até o file appSettings.json em ...\AdaTech.AIntelligence.Presentation\AdaTech.AIntelligence.WebAPI\appsettings.json (dentro da pasta src)
2. Neste arquivo, modifique o valor atribuído a ApiKey para uma chave válida de integração com o Vision do ChatGPT-4

```bash
  "ApiKey": "sua-chave-aqui",
```


3. Em UserCredentials, modifique o valor atribuído ao UserName e Password

```bash
  "UserCredentials": {
    "UserName": "seu-email-aqui",
    "Password": "SuaS3nh@Aqu1"
  },
```

UserName será seu login de acesso ao sistema. Esta alteração criará para você um usuário com todas as permissões do sistema para facilitar seu uso dentro dela.

4. Em EmailSettings, modifique o Domain para o domain da sua empresa, para que apenas os funcionários (com email corporativo) consigam acessar

```bash
  "EmailSettings": {
    "Domain": "@me.com.br"
  },
```

Todos os usuários só poderão se registrar com um email com este domínio e só poderão logar se este email for válido e confirmado via link enviado por email.

5. Em BaseOCRUrl, verifique se a porta (7034) é a mesma que aparece no arquivo abaixo no seu projeto (ocr>AdaTech.AIntelligence.OCR.Presentation>AdaTech.AIntelligence.OCR.WebAPI>Propities>launchSettings.json) na rota https:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/153667c2-5385-453f-a4c9-e304d856a95f)


#### Atualizando o Banco:

1. Abra o seu terminal e rode o comando para atualizar o banco de acordo com as migrations fornecidas.

Para o PowerShell do Desenvolvedor (basta abrir o terminal em View):
```bash
cd .\AdaTech.AIntelligence.WebAPI

dotnet ef database update --project ..\AdaTech.AIntelligence.DateLibrary
````

Para o Package Manager Console:

- Coloque o DateLibrary como StartProject e rode o comando abaixo:

```bash
Update-Database
```

2. Neste momento é comum acontecer alguns erros, caso ocorra algum erro que não esteja sabendo resolver, digite no terminal:

```bash
dotnet ef
```

Caso um erro também ocorra aqui, verifique se atualizou o seu RunTime para a versão mais recente: 8.0.2

Abaixo segue o link para download caso necessite:
[Runtime 8.0.2](https://dotnet.microsoft.com/pt-br/download)

Se tudo ocorrer bem, pronto! Estamos com o banco atualizado!!!

#### Configurar Inicializações:

1. No Visual Studio, abra o Soluctionm Explorer e clique com o botão direito na soluction.
2. Abra "Configure Startup Projects"

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/82e405e0-a3c4-4bfe-8b58-5219ea6d75d0)

3. Na janela que abrirá selecione a opção: `Multiple start projects`
4. Dentre os projetos, selecione: AdaTech.AIntelligence.WebAPI (Action em Start) e AdaTech.AIntelligence.OCR.WebAPI (Action em Start without debugging)

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/53529ba1-b862-4374-895f-3d22d0af2a9e)

Clique em aplicar e depois confirme!

### Passo 3: Preparando o Banco

Agora que seu banco está devidamente configurado, abra a aba de pesquisa na navegação superior: Search

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/1ac45469-f9ca-472c-99bd-8e6a681f54da)

Pesquise por: `SQL Server Object Explorer`

Algo clicar no resultado obtido, fixe-o na tela. Deve estar vendo algo como a imagem abaixo:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/15260a14-e542-4e09-ab8e-7270477782d3)

ExpenseReporting é o nosso banco em uso. Para visualizar suas tabelas, basta clicar com o botão direito e selecionar View Data, como abaixo:

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/cd6917d1-aef6-4fe1-9e13-ed4dfcab8945)

A tabela acima se refere aos usuários do sistema. Note que, para você ela está completamente vazia, o que poderia ser um problema para a utilização do projeto, já que algumas de nossas rotas são fechadas, sendo acessadas somente com as permissões adequadas. Se desejar ver que permissões são, basta abrir a tabela de Roles e se quiser saber quais permissões cada usuário tem, basta abrir UsersRoles.

Para evitar fadiga, o primeiro usuário é criado assim que o programa é iniciado. Lembra do nosso appsettings que pedimos para modificar? As modificações contidas em UserName e Password seram os dados do seu usuário (que terá por nome FinancialAdmin - tendo todas as permissões do sistema).

Agora, no seu banco, você deve estar vendo algo como (depois de abrir o view data):

![image](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/93644115/dccc4305-b1d2-4445-814e-fe6a86587dff)

Caso ainda não esteja vendo, experimente dar um refresh em seu banco clicando na seta circular azul. Se entrar em UserRole notará que esse usuário possui duas regras: Admin e Finance. Essas duas regras em conjunto permitem acesso a qualquer endpoint do sistema.

### Passo 4: Testando o sistema:

Quando as funcionalidades do sistema, voltemos agora ao README, onde veremos mais sobre elas: basta clicar aqui -> [README](https://github.com/tauanyfeitosa/AdaTech.AIntelligence/blob/master/README.md)

## 🔧 Tecnologias Utilizadas

|  Tecnologia                         |                                   Para que serve?                |  Versão   |               Cuidados                                                                           |
| :-----------------------------: | :------------------------------------------------------------------: | :-------: | :----------------------------------------------------------------------------------------------: |
|`EntityFramework Core`           |       facilita a interação entre uma aplicação e um banco de dados   | 8.0.2.    |  Tenha cuidado para que seu Runtime esteja na mesma versão ou superior                           |
|`EntityFramework Core Tools`     |       facilita o update do banco de dados                            | 8.0.2.    |  Utilize essa ferramenta para  atualizar seu banco com todas as migrações disponíveis no projeto |
|`EntityFramework Core Design`    |       facilita as migrações do banco de dados                        | 8.0.2.    |                                                                                                  |
|`EntityFramework Core SQL Server`|       facilita a interação entre uma aplicação e um banco de dados   | 8.0.2.    |  Por não se tratar de um arquivo como o SQLite, tenha ciência que seu banco iniciará vazio       |
|`AspNetCore Identity UI`         |contém a interface de usuário (UI) padrão para o sistema de identidade| 8.0.2.    |  Tem seus próprios métodos de login, logout, confirmação de email entre outros                   |
|`Swashbuckle.AspNetCore`         |gera documentação para APIs usando o formato OpenAPI (Swagger)        | 6.4.0     |                                                                                                  |
|`AspNetCore.Identity.EntityFrameworkCore`|extensão do ASP.NET Core Identity com implementações específicas para o Entity Framework Core | 8.0.2.    |Atualizações de segurança são frequentes nesta área; portanto, mantenha-se atualizado com as versões mais recentes.|
|`Microsoft.AspNetCore.Http`      |contém as principais abstrações para lidar com HTTP no ASP.NET Core   | 2.2.2     |cuidado os dados da sessão e cookies para evitar vulnerabilidades de segurança, como CSRF (Cross-Site Request Forgery) ou XSS (Cross-Site Scripting)|
|`Microsoft.Extensions.Http`      |oferece funcionalidades para a criação e configuração de instâncias de HttpClient   | 8.0.0     |Evite criar uma nova instância de HttpClient para cada solicitação; em vez disso, use IHttpClientFactory|
|`Microsoft.AspNetCore.Mvc`       |fundamental para o desenvolvimento de aplicações web e APIs no ASP.NET Core| 2.2.0     |Tenha cuidado com a exposição de dados sensíveis por meio de APIs e a serialização automática de modelos que podem conter dados que não devem ser expostos|
|`Extensions.Options.ConfigurationExtensions`|fornece extensões para a interface IOptions<T>             | 8.0.0.    |Evite expor quaisquer informações de configuração sensíveis, como chaves de API ou segredos de banco de dados, especialmente em logs ou exceções.|
|`Newtonsoft.Json`                |biblioteca popular para trabalhar com JSON no .NET                    | 13.0.3    |Fique atento a vulnerabilidades de deserialização que podem ser exploradas se as entradas do usuário não forem devidamente validadas|
|`Swashbuckle.AspNetCore.Swagger` |integra a documentação da API Swagger e interfaces de usuário interativas diretamente em suas aplicações web| 6.4.0    |é importante garantir que a documentação do Swagger não revele informações sensíveis e seja protegida em ambientes de produção|


###Para todos os pacotes:
1. Manter os pacotes atualizados para a versão mais recente pode ajudar a mitigar problemas de segurança conhecidos e bugs.
2. Use a análise de código estático e outras ferramentas de segurança para identificar e resolver problemas proativamente.
3. Esteja ciente das implicações de atualizações de versão, pois podem incluir alterações significativas que podem afetar a estabilidade e segurança da sua aplicação.
4. Quanto ao banco, tenha em mente que não é um arquivo como o SQLite e sim um banco mais complexo e que funciona apenas localmente dentro deste projeto. Assim, seu banco iniciará vazio e deverá alimentá-lo através do projeto.





# Security Policy

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


### Para todos os pacotes:
1. Manter os pacotes atualizados para a versão mais recente pode ajudar a mitigar problemas de segurança conhecidos e bugs.
2. Use a análise de código estático e outras ferramentas de segurança para identificar e resolver problemas proativamente.
3. Esteja ciente das implicações de atualizações de versão, pois podem incluir alterações significativas que podem afetar a estabilidade e segurança da sua aplicação.
4. Quanto ao banco, tenha em mente que não é um arquivo como o SQLite e sim um banco mais complexo e que funciona apenas localmente dentro deste projeto. Assim, seu banco iniciará vazio e deverá alimentá-lo através do projeto.

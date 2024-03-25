# Antes de tudo
Ao rodar a aplicação, substitua a url local do swagger pela url abaixo para que possa interagir com o sistema de maneira mais agradável e fluida.

```
https://localhost:7016/templates/authentication/register.html
```
Ou então clique nos links abaixo, caso a aplicação já esteja rodando.
- [Tela de registro de usuário](https://localhost:7016/templates/authentication/register.html)
- [Tela de login de usuário](https://localhost:7016/templates/authentication/login.html)

Veja nossas funcionalidades e aproveite o AIntelligence!

# Funcionalidades
## Registro de usuário
Na tela de registro de usuário é preciso inserir algumas informações básicas sobre este novo usuário, e internamente são feitas algumas validações como:
1. Usuário com domínio de e-mail válido e que não conste no banco de dados;
2. CPF de usuário válido e que não conste no banco de dados;
3. Senha forte (que contém letras maiúsculas, minusculas, caracteres especiais e números);
4. Data de nascimento que represente uma pessoa com ao menos 14 anos (considerando que a empresa pode conter jovens aprendizes).
<img width="960" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/0a461df4-9f74-48b1-8e95-6fa22d822c05">

## Login
Esta tela nos permite acessar as ações do sistema de fato, mas lembre-se de confirmar seu e-mail antes de fazer o login!
Caso não lembre e ainda assim tente efetuar o login, é exibida uma mensagem do sistema para te lembrar caso seja este o caso.
<div>
  <img width="960" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/65427765-87a9-4546-8f25-59e34bb9a28d">
</div>

## Home Page
### Funcionário
Um funcionário comum pode registrar uma despesa e tem acesso apenas às suas despesas. O sistema bloqueia o acesso de um funcionário comum às páginas de "Financeiro" e "Administrativo", pois são áreas restritas para funcionários que possuem, respectivamente, os cargos de "Finance" e "Admin".

<div>
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/1bd2c56f-50a7-4097-8dfe-8967955c6132">
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/1e516cf7-72f9-46fb-be12-eaf2aeeda012">
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/1a61ddec-9890-45a5-8e5d-e5a260b0a087">
</div>

Para registrar uma despesa é possível enviar uma imagem por arquivo ou por URL da imagem.
<div>
  <img width="480" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/91cf0202-1236-46e7-a554-90732c174cd5">
  <img width="480" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/78006252-71de-46d6-b633-b413bdf35b42">
</div>

Após clicar em "Criar despesa", aguarde alguns segundos e logo em seguida a sua despesa será criada e aparecerá na tabela de "Minhas despesas".
<div>
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/133d16e1-1ca9-4b1c-afc8-ec7b086cd43d">
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/762f3807-f339-45f0-9c30-b2e2a9c52cbd">
  <img width="320" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/e011e76c-90c7-4a34-b665-fb4a850e6c21">

</div>

### Financeiro
O funcionário do setor financeiro tem acesso a todas as notas fiscais pendentes de avaliação enviadas para o sistema. Este funcionário pode atualizar os estados das notas submetidas para pagas, desativar notas e solicitar evolução de cargo.

### Administrador
O administrador tem acesso ao gerenciamento dos usuários. Este tipo de funcionário pode criar usuários com qualquer cargo ou permissões internas, e é ele quem aprova ou rejeita as solicitações de evolução de cargo.

## Documentação
Ao clicar no botão de documentação você é redirecionado para a nossa documentação técnica, feita no README.md do projeto. Esta documentação apresenta:
1. Seção para links úteis;
2. Descrição do projeto;
3. Como usar o projeto;
4. Explicação breve sobre o que é o Swagger;
5. Explicação breve sobre como construir e o que é uma OCR;
6. Lembretes importantes sobre o uso do Swagger nesta aplicação;
7. Fluxo de funcionamento;
8. Permissões do sistema.
<div>
  <img width="960" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/16b32624-8e4a-43df-9d58-d01286018d43">
</div>


## Contate-nos
Nesta página é possível ver cada contribuidor do nosso projeto com um link que o reencaminha para o seu respectivo perfil do LinkedIn.

Nos mande uma mensagem! Caso queira nos dar uma crítica construtiva, sugestão ou um elogio sobre este projeto, estamos abertos para conversar ;)
<div style="display: flex; justify-content: center;">
      <img width="300" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/a3d8d0d5-6357-4054-8e6f-238067757d11">
      <img width="300" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/98af060a-1366-46bf-96e8-343cc8271227">
      <img width="300" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/388b7797-b771-438d-8d60-998a794ce724">
      <img width="300" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/e601a3e6-f999-4535-a364-9a5ad0c89387">
      <img width="300" alt="image" src="https://github.com/tauanyfeitosa/AdaTech.AIntelligence/assets/49692286/151a0448-dc29-4e68-be8f-fe175fdf0729">
</div>

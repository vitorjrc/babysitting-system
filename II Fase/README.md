# Especificação - Segunda Fase




## Clientes

### Verificar lista de profissionais
#### Definição de requisitos de utilizador
* O utilizador deve poder consultar a lista de profissionais existentes no sistema.

#### Especificação de requisitos de sistema
* O sistema mostra a lista dos profissionais registados no sistema, bem como a disponibilidade e seus ratings;

* O sistema disponibilizará a lista dos profissionais registados no sistema ordenados por raking ou por número de trabalhos prestados;

* Por profissional, deverão ser fornecidos pelo sistema alguns dados pessoais.

### Agendar trabalho (com base na lista do site)
#### Definição de requisitos de utilizador
* O utilizador deve poder agendar um trabalho, com base na lista de profissionais.

#### Especificação de requisitos de sistema
* O sistema deve permitir ao utilizador selecionar o profissional que pretende;
* O sistema deve permitir ao utilizador verificar o custo do agendamento, antes de finalizar a operação, que será debitado depois do profissional aceitar;
* O sistema deve permitir selecionar as datas em que o serviço irá ser realizado;
* O sistema deve recorrer à API externa do Bing Maps para conhecer a localização do utilizador ou então obriga a que a localização seja introduzida manualmente;
* Deverá autorizar a inserção de observações por parte do utilizador, para especificar algumas exigências do mesmo no que toca ao serviço;
* O sistema deverá permitir que seja mandada uma proposta de serviço devidamente especificada ao profissional, que, posteriormente, poderá aceitar ou recusar.

### Avaliar trabalho (nota: se vai avaliar trabalho, tem q ter área de trabalhos passados)
#### Definição de requisitos de utilizador
* O utilizador deve poder avaliar um trabalho de um profissional.

#### Especificação de requisitos de sistema
* O sistema deve mostrar os parâmetros classificativos essenciais à avaliação e deve permitir que o utilizador os consiga preencher;
* Armazenar a média dos parâmetros na base de dados;

### Realizar pagamento (ao infantário, não ao babysitter)
#### Definição de requisitos de utilizador
* O utilizador deve poder realizar o pagamento do serviço, antes da realização do mesmo (depois de confirmado o agendamento).

#### Especificação de requisitos de sistema
* O sistema deverá permitir que seja elaborado um método de pagamento (online), que será sempre a favor do infantário (que acordará depois o pagamento com os respectivos profissionais). Este método é utilizado aquando do agendamento do trabalho.


### Beneficiar de descontos - ser golden ou não
#### Definição de requisitos de utilizador
* O utilizador deve poder beneficiar de descontos, conforme o seu estatuto de cliente golden (com filho/a inscrito no infantário) ou externo.

#### Especificação de requisitos de sistema
* O sistema deverá permitir marcar os clientes Golden com o referido estatuto, para futuros descontos em agendamentos.





## Profissionais

### Verificar perfil
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar e alterar o seu perfil.

#### Especificação de requisitos de sistema
* O sistema deverá fornecer o perfil de utilizador completo, com suporte à edição dos dados e armazenamento de determicadas alterações.


### Verificar propostas de trabalhos
#### Definição de requisitos de utilizador
* O utilizador deverá ser capaz de consultar a lista de propostas de novos trabalhos.

#### Especificação de requisitos de sistema
* O sistema deve fornecer uma lista de propostas de serviços, com a opção de recusa ou aceitamento.


### Verificar trabalhos agendados
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar os trabalhos agendados.

#### Especificação de requisitos de sistema
* O sistema deverá fornecer a lista de trabalhos agendados, juntamente com toda a informaçoa pertinente (data, horário, observações, localização)


### Ver direções até casa do cliente
#### Definição de requisitos de utilizador
* O utilizador deve poder ver direções até à casa do cliente. Deve também ser apresentado um mapa com representativo da morada. Para além disso, deve poder visualizar a trajetória até à residência do cliente.

#### Especificação de requisitos de sistema
* O sistema deve recorrer-se da API externa disponibilizada pelo Bing Maps de forma a representar a localização do cliente, bem como a trajetória até a este.




## Admin

### NOTA 1: 
* Admin, profissional e cliente podem fazer login.
* Admin regista profissionais e a ele próprio.
* Cliente pode registar-se

### NOTA 2: 
* O admin já vê as listas todas de clientes, profissionais, serviços, ...

### Registo no sistema
#### Definição de requisitos de utilizador
* O utilizador deve poder registar-se no sistema, assim como registar profissionais.

#### Especificação de requisitos de sistema
* O sistema deve solicitar email/username e password;
* O sistema não deve permitir o registo de utentes com um username/email já registado;
* O sistema deve armazenar os dados na base de dados.


### Autenticação no sistema
#### Definição de requisitos de utilizador
* O utilizador deve conseguir autenticar-se no sistema.

#### Especificação de requisitos de sistema
* O sistema deve solicitar email/username e password para se autenticar;
* O sistema deve verificar a validade dos dados inseridos e associar os dados a um perfil de utilizador.


### Marcar clientes Golden
#### Definição de requisitos de utilizador
* O utilizador deverá poder marcar, dentro da lista dos clientes, os que são Golden ou não.

#### Especificação de requisitos de sistema
* O sistema disponibilizará a opção de poder marcar clientes Golden.


### Gerir pagamento
#### Definição de requisitos de utilizador
* O utilizador deverá poder gerir a lista de trabalhos já efetuados, com o propósito de consultar as despesas e, posteriormente, contactar o infantário.

#### Especificação de requisitos de sistema
* O sistema disponibilizará a lista de serviços já aceites, juntamente com os detalhes do mesmo.
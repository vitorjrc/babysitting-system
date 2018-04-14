# Especificação - Segunda Fase




## Clientes

### Verificar lista de profissionais
#### Definição de requisitos de utilizador
* O utilizador deve poder consultar a lista de profissionais existentes no sistema.

#### Especificação de requisitos de sistema
* O sistema mostra a lista dos profissionais registados no sistema, bem como os seus rankings e horário de trabalho (turno);

* O sistema disponibilizará a lista dos profissionais registados no sistema ordenados por rating, anos de trabalho ou número de serviços prestados;

* Por profissional, deverão ser fornecidos pelo sistema alguns dados pessoais.

### Agendar trabalho (nota: cada tipo de serviço já terá um preço definido pelo infantario para questoes de simplificaçao --- TURNOS M T N MANHA(8-16) TARDE(16-24) NOITE(24-8))
#### Definição de requisitos de utilizador
* O utilizador deve poder agendar um trabalho, com base na data, hora e morada requerida, e com base na lista de profissionais disponíveis para a hora mencionada.

* O utilizador terá à sua disposição um conjunto de serviços extra que poderá, ou não, optar por requerir, acrescendo ao preço total do trabalho.

#### Especificação de requisitos de sistema
* O sistema deve permitir selecionar as datas em que o serviço irá ser realizado, assim como a hora desejada para o mesmo ser realizado;
* O sistema deve recorrer à API externa do Bing Maps para conhecer a localização do utilizador ou então obriga a que a localização seja introduzida manualmente;
* Deverá autorizar a inserção de serviços complementares por parte do utilizador, para especificar algumas exigências do mesmo no que toca ao serviço;
* O sistema deve permitir ao utilizador selecionar o profissional que pretende;
* O sistema deve permitir ao utilizador verificar o custo do agendamento, antes de finalizar a operação, que será debitado depois do profissional aceitar;
* O sistema deverá permitir que seja mandada uma proposta de serviço devidamente especificada ao profissional, que, posteriormente, poderá aceitar ou recusar.


### Verificar trabalhos anteriores
#### Definição de requisitos de utilizador
* O utilizador deve conseguir aceder a informação detalhada de serviços já realizados anteriormente.

#### Especificação de requisitos de sistema
* O sistema deverá permitir observar uma listagem de todos os trabalhos realizados anteriormente à data de um cliente específico.


### Avaliar trabalho (nota1: se vai avaliar trabalho, tem q ter área de trabalhos passados. nota2: avaliaçao decimal, nao inteira)
#### Definição de requisitos de utilizador
* O utilizador deve poder avaliar um trabalho, já realizado, de um profissional.

#### Especificação de requisitos de sistema
* O sistema deve mostrar os parâmetros classificativos essenciais à avaliação e deve permitir que o utilizador os consiga preencher;
* Armazenar a média dos parâmetros na base de dados;

### Realizar pagamento (ao infantário, não ao babysitter)
#### Definição de requisitos de utilizador
* O utilizador deve poder realizar o pagamento do serviço (em dinheiro ou online (cartão)), antes da realização do mesmo, e depois de confirmado o agendamento).

#### Especificação de requisitos de sistema
* O sistema deverá permitir que seja elaborado um método de pagamento (online), que será sempre a favor do infantário (que acordará depois o pagamento com os respectivos profissionais). Este método é utilizado aquando do agendamento do trabalho.


### Beneficiar de descontos - ser golden ou não
#### Definição de requisitos de utilizador
* O utilizador deve poder beneficiar de descontos, conforme o seu estatuto de cliente golden (com filho/a inscrito no infantário) ou externo.

#### Especificação de requisitos de sistema
* O sistema deverá permitir marcar os clientes Golden com o referido estatuto, para futuros descontos em agendamentos.


### Verificar trabalhos agendados
#### Definição de requisitos de utilizador
* O utilizador poderá analisar os trabalhos agendados, com o objetivo de verificar os detalhes de agendamento.

#### Especificação de requisitos de sistema
* O sistema deverá permitir a disponibilização de uma lista de serviços de babysitting agendados, com os respetivos detalhes.


### Verificar trabalhos propostos
#### Definição de requisitos de utilizador
* O utilizador deve poder analisar os trabalhos propostos (ainda não aceites), com o intuito de verificar os detalhes de agendamento (hora, data, ...), ou até cancelar o mesmo.

#### Especificação de requisitos de sistema
* O sistema deverá permitir a disponibilização de uma lista de serviços de babysitting propostos, juntamente com os respetivos detalhes, e permitir a opção de cancelamento do mesmo.


### Verificar perfil
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar o seu perfil, que deverá conter a informação pessoal inserida aquando do registo, assim como as listagens de trabalhos propostos, agendados e já realizados.

#### Especificação de requisitos de sistema
* O sistema deverá fornecer o perfil de utilizador completo e intuitivo.








## Profissionais

### Verificar perfil
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar e alterar o seu perfil, no qual deverá constar a sua informação pessoal, assim como as listagens de trabalhos propostos, agendados e já realizados.

#### Especificação de requisitos de sistema
* O sistema deverá fornecer o perfil de utilizador completo, com suporte à edição dos dados e armazenamento de determicadas alterações.


### Verificar propostas de trabalhos
#### Definição de requisitos de utilizador
* O utilizador deverá ser capaz de consultar a lista de propostas de novos trabalhos, com o intuito de os aceitar ou recusar.

#### Especificação de requisitos de sistema
* O sistema deve fornecer uma lista de propostas de serviços, com a opção de recusa ou aceitamento.


### Verificar trabalhos agendados
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar os trabalhos agendados.

#### Especificação de requisitos de sistema
* O sistema deverá fornecer a lista de trabalhos agendados, juntamente com toda a informaçoa pertinente (data, horário, localização e possível realização de serviços extra)


### Ver direções até casa do cliente
#### Definição de requisitos de utilizador
* O utilizador deve poder ver direções até à casa do cliente. Deve também ser apresentado um mapa com representativo da morada. Para além disso, deve poder visualizar a trajetória até à residência do cliente.

#### Especificação de requisitos de sistema
* O sistema deve recorrer-se da API externa disponibilizada pelo Bing Maps de forma a representar a localização do cliente, bem como a trajetória até este.


### Verificar trabalhos anteriores
#### Definição de requisitos de utilizador
* O utilizador deve conseguir aceder a informação detalhada de serviços já realizados anteriormente, onde, para além da informação do mesmo, poderá verificar a sua avaliação.

#### Especificação de requisitos de sistema
* O sistema deverá permitir observar uma listagem de todos os trabalhos realizados anteriormente à data de um profissional específico, bem como a sua avaliação.







## Admin

### NOTA 1: 
* Admin, profissional e cliente podem fazer login.
* Admin regista profissionais (e ele próprio).
* Cliente pode registar-se.

### NOTA 2: 
* O admin já vê as listas todas de clientes, profissionais, serviços, ... -> Generalizar num só use case e requisito -> Generalizar trabalhos também num só use case e requisito

### NOTA 3:
* O login é geral às três entidades, acrescentar nas de cima. Acrescentar também o registo no cliente.


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


### Verificar lista de utilizadores do sistema
#### Definição de requisitos de utilizador
* O utilizador deverá poder ver e gerir a lista de utilizadores do sistema.

#### Especificação de requisitos de sistema
* O sistema disponibilizará a lista de utilizadores, juntamente com detalhes dos mesmo.


### Verificar lista geral de trabalhos
#### Definição de requisitos de utilizador
* O utilizador deverá poder verificar e gerir a lista de trabalhos geral - já efetuados, agendados e propostos.

#### Especificação de requisitos de sistema
* O sistema disponibilizará a lista de serviços, juntamente com detalhes dos mesmos.
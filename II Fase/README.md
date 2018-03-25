# Especificação - Segunda Fase

## Requisitos
### Verificar lista de profissionais
#### Definição de requisitos de utilizador
* O utilizador deve poder consultar a lista de profissionais existentes no sistema.

#### Especificação de requisitos de sistema
* O sistema mostra a lista dos profissionais registados no sistema, bem como a disponibilidade??? e seus ratings;

### Agendar trabalho (com base na lista do site)
#### Definição de requisitos de utilizador
* O utilizador deve poder agendar um trabalho, com base na lista.

#### Especificação de requisitos de sistema
* O sistema deve permitir ao utilizador selecionar o profissional que pretende;
* O sistema deve permitir selecionar as datas em que o serviço será ser realizado;
* O sistema deve recorrer à API externa do Bing Maps para conhecer a localização do utilizador ou então obriga a que a localização seja introduzida manualmente;
* Deverá autorizar a inserção de observações por parte do utilizador;

### Avaliar trabalho
#### Definição de requisitos de utilizador
* O utilizador deve poder avaliar um trabalho e/ou avaliar um profissional.

#### Especificação de requisitos de sistema
* O sistema deve mostrar os parâmetros classificativos essenciais à avaliação e deve permitir que o utilizador os consiga preencher;
* Armazenar a média dos parâmetros na base de dados;

### Realizar pagamento (ao infantário, não ao babysitter)
#### Definição de requisitos de utilizador
* O utilizador deve poder realizar o pagamento do serviço.

#### Especificação de requisitos de sistema
*

### Verificar perfil
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar o seu perfil.

#### Especificação de requisitos de sistema
* O sistema

### Verificar trabalhos agendados
#### Definição de requisitos de utilizador
* O utilizador deve poder verificar os trabalhos agendados.

#### Especificação de requisitos de sistema
*

### Ver direções até casa do cliente
#### Definição de requisitos de utilizador
* O utilizador deve poder ver direções até à casa do cliente. Deve também ser apresentado um mapa com representativo da morada. Para além disso, deve poder visualizar a trajetória até à residência do cliente.

#### Especificação de requisitos de sistema
* O sistema deve recorrer-se da API externa disponibilizada pelo Bing Maps de forma a representar a localização do cliente, bem como a trajetória até a este.

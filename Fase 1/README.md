# babysitting-system

Repositório para o projeto de LI4 de 2017/2018. Consiste num sistema de agendamento de babysitters 24/7, com o apoio de um site para os utilizadores e aplicação móvel para funcionários.
Os funcionários devem ter acesso a um mapa onde podem verificar as localizações dos seus agendamentos.

## Motivo

* Porquê fazer?
* O que é suposto resolver?

## Features

### Clientes

* Verificar lista de profissionais
* Agendar trabalho (com base na lista do site)
* Avaliar trabalho
* Realizar pagamento (ao infantário, não ao babysitter)

### Profissional

* Verificar perfil
* Verificar trabalhos agendados
* Ver direções até casa do cliente
* (Recebe pagamento/ordenado mensal do infantário apenas)

## Organização do relatório

### 1. Contextualização (CAMPOS)

Uma rede de infantários de luxo pretende criar um serviço de babysitting. As encarregadas acompanharão um determinado pupilo e, a pedido dos pais, sendo o serviço pago hora a hora, quer dentro ou fora do horário do infantário.

### 2. Motivação e objetivos (CAMPOS)

Contratados pela empresa, a empresa quer aumentar seus lucros e melhorar qualidade do serviço.

### 3. Definição da identidade do sistema a desenvolver (DIANA)

É um sistema de gestão de serviço de acompanhamento personalizado, providenciado pelo infantário "Crianças de Luxo", cuja aplicação se chamará "GuguDadah". Os pais vão poder requisitar serviços (e determinar a hora dos mesmos), face aos seus filhos e respetivos educadores (que, a partir da primeira utilização, vão tentar ser mantidos). Os educadores vão consultar as suas tarefas e o miúdos. Os pais vão poder controlar o estado atual do serviço a ser executado, e custo associado.

### 4. Análise de Viabilidade (MARCOS)

```text
Rascunho

É viável fazer sistema? quanto vai ganhar a nossa empresa? vai reduzir fluxo de papel? ter informação 24h acessível? vai permitir libertar recursos?
A empresa fez uma análise de mercado e verificou que era rentável a produção da aplicação e posterior serviço, tendo-nos contratado.... (desenvolver)

Em que medida nos como empresa vamos beneficiar (não é o foco)
Quem constitui a procuração para o nosso serviço (pais para os miúdos)
Se tem pernas para andar, se é viável a longo termo, se é um serviço que perdure independentemente do passar do tempo e das "modas"
Procura *
Se conseguimos manter o serviço

diz as cenas face ao ganho do infantario
-> permite um serviço diferenciador e de excelência
-> melhora a motivação dos putos para ir à escola
merdas assim
é na mm ordem de ideias que o reduzir papel etc
```

### 5. Identificação dos recursos necessários (VITOR)

Recursos necessários (pessoas necessárias), entrevistas (pais, encarregados, infantario), contactos.
(não falar de ferramentas, é óbvio)

### 6. Modelo de sistema (maquete) (MARCOS V1)

```text
* não é use cases, etc
* é suposto dizer como o sistema vai funcionar num conjunto de caixas, imagem q explique funcionamento
* user mete dados, o q guardo no sistema, conjunto de pedidos em q tenho uma plataforma móvel, etc
* tb posso dizer , na parte do q guardo do sistema, posso dizer ah tenho uma impressora, é aqui q se guardam as faturas, tickets, .....
* falar de quando é feito o acknowlegment
* BONECO, CAIXA, CAIXA, CAIXA, ..., BONECO e descrever cada caixa/etapa (pode se desenhar tb a cloud/seridor )
```

### 7. Def medidas de sucesso (SERGIO ponto 1,2   ///   VITOR ponto 3,4)

* Software deve responder aos requisitos do cliente
* Execução dentro do tempo proposto
* (+ pesquisar success measures)
* (é possível que haja vários serviços e na próxima fase podemos descrever apenas parte dos serviços)

### 8. Plano de desenvolvimento (DIANA)

* Diagrama de Gantt
* enumerar tarefas/descrever e fazer diagrama
* nota: contabilizar o tempo real que gastamos nisto, para ver o custo do projeto (converter hora em €)

### 9. Organização do Documento (SERGIO)

* Esta secção só aparecerá no fim do trabalho

### 10. Conclusões e trabalho futuro (VITOR)

## Notas

* **Pesquisar subfuncionamento dos subsistemas para melhor fundamentar o projeto**
* empresa tipo airbnb, uber, nao tem posse de nada, oferece serviços. pessoal especializado e certificado.
* fotos de hora a hora, videos
* profissionais avaliados pelos pais, ranking
* nome empresa: GuguDada
* "De pais, para pais"

## Ideia

* Os pais que tenham os filhos a frequentar o infantário têm desconto em serviços ao domicílio de qualquer tipo.
* Todos os utilizadores têm um "perfil" na aplicação que pode conceder uma acumulação de "pontos(?)" que dá desconto ao fim de x serviços.

***

### Análise de Requisitos - 2 pessoas

* Requisitos Utilizador -> Diana
* Requisitos Sistema (funcionais e não funcionais) -> Sérgio

### Modelação UML - 5 pessoas

* Use Cases + Especificação -> Diana
* Sequência -> Vítor
* Classe -> Marcos
* Atividade -> Campos
* Estado -> Sérgio

### Base de Dados - 2 pessoas

* Modelo Concetual (tópicos de BD todos) -> Vítor
* Modelo Lógico (tópicos de BD todos) -> Campos

### Mockups - 1 pessoa

* Design interface -> Marcos

-> Atualizar Diagrama de Gantt (+Mockups)

-> Apontar datas finais de cada tópico

-> Apontar horas de trabalho

## Horas de trabalho conjuntas:
	-19/02/18: 1h00
	-20/02/18: 1h30
	-21/02/18: 2h00
	-28/02/18: 1h30
	-
	-
	-
	-
	-
	-



## Horas de trabalho individuais ( pessoa/data/#nHoras ):
	-Diana/22/02/18/1h
	-Diana/24/02/18/2h
	-Vitor/21/02/18/1h
	-Vitor/.../2h
	-Campos/21/02/18/1h
	-Sérgio/21/02/18/1h
	-Marcos/.../1h
	-Sérgio/26/02/18/0.30h
	-Diana/26/02/18/0.30h
	-Diana/28/02/17/0.30h
	-Campos/04/03/18/1h
	-Sérgio/01/03/18/1h
	-Sérgio/02/03/18/4h
	-Sérgio/03/03/18/4h
	-Sérgio/04/03/18/6h
	-Vitor/03/03/18/2h
	-Vitor/03/05/18/2h
	-Sérgio/05/03/18/3h
	-Sérgio/06/03/18/2h

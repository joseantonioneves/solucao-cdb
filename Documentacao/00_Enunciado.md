# Enunciado Teste para Desenvolvedor C#/Angular Fullstack

## 1. Objetivo

- Implementar uma solução fundamentalmente pelos princípios do SOLID, Testes unitários e de desempenho.

## 2. Desafio do cálculo do CDB

a. Solicito a criação de uma tela WEB onde se possa informar um valor monetário positivo, e um prazo em meses maior ou igual a 1 para resgate da aplicação. Após solicitar o cálculo de investimento, a tela deve apresentar o resultado bruto e líquido do investimento.

b. É desejo a criação de uma WEB API que receba os dados informados na cláusula (a) .
      i. Para cálculo do CDB, deve-se usar a fórmula VF = VI x [1+(CDB x TB)] onde:
         - VF é o valor final;
         - VI é o valor inicial;
         - CDI é o valor dessa taxa no último mês;
         - TB é o valor de quanto o banco paga sobre o CDI.
         Obs. A fórmula calcula somente o valor de um mês, ou seja, os rendimentos de cada mês devem ser utilizados para calcular o mês seguinte.
        ii. Para medida do protótipo considerar os valores abaixo como fixos:
            - TB = 108%;
            - CDI = 0,9%.
        iii. Para Cálculo do imposto usar a seguinte tabela:
             - até 6 meses: 22,5%
             - até 12 meses: 20%
             - até 24 meses: 17,5%
             - Acima de 24 meses: 15%

## 3. DOD da Avaliação

- O ferramental de desenvolvimento será o Visual Studio Code;
- Ambos os projetos devem estar em uma única solução (.sln) para facilitar a avaliação;
- A solução deve ter um arquivo readme.mk com instruções para a execução de testes da solução; 
- A solução deve ter um front-end e um back-end;
- O Back-end deve ser usado o .NET 8.0 e implementado em C#;
- O projeto do Front-End deve ser implementado em Angular CLI;
- A camada do Back-End (WEB API) deve ter uma cobertura de testes acima de 90% na camada lógica;
- O Teste Unitário no Angular é um StrechGo;
- Os projetos não devem possuir alertas de análise de código nativas do Visual Studio, tão pouco de regras padrão do Sonar. Recomenda-se a extensão do SonarLint;
- O código será versionado em repositório público no GitHub
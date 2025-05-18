# Roteiro DevOps

1. ``` git checkout develop ```
   
2. ```git pull```
   
3. ```git checkout -b feature/nome-da-feature-d```
   
4. Faça as suas implementações e commits nessa nova branch
   
5. Ao finalizar todos os seus commits, roda a aplicação, testa as     suas alterações, verifica se não quebrou outra parte do código sem querer e atualiza com as mudanças que podem ter subido para develop enquanto você trabalhava na sua branch com o comando git pull origin develop.
   
6. Agora que o trabalho na sua branch está finalizado e ela está atualizado com a develop, envie-a para o devops com o comando de push. Se estiver enviando pela primeira vez: ```git push --set-upstream origin feature/nome-da-feature-d```, se for da segunda vez pra frente basta: ```git push```.
   
7. No DevOps, ir nas branches do repositório, encontrar a branch ```feature/nome-da-feature-d``` e criar um novo pull request. 
   
8. Neste, a branch destino deve ser a develop e você precisa preencher campos obrigatórios como título e descrição. 
   
9.  Após isso, só criar o pull request e marca-lo como set-autocomplete se atentando no tipo de commit que deve ser squash commit e verificar se a opção de deleção da branch de trabalho está marcado.
    
10.  Finalizado os procedimentos acima, basta enviar o PR para revisão.

# ShabbatBrunch

### Introduction

Bonjour à tous,

Au terme de cette formation intense en C# .NET, notre expertise nouvellement acquise s'exprime pleinement à travers un projet chef-d'œuvre qui encapsule l'essence même de nos apprentissages. En déployant des concepts avancés tels que la programmation orientée objet, la manipulation de bases de données avec Entity Framework, la création d'interfaces utilisateur dynamiques avec WPF, et la gestion de services avec ASP.NET, notre projet démontre notre maîtrise complète du framework.

Le code élaboré reflète une compréhension approfondie des bonnes pratiques de développement, avec une architecture solide, des performances optimisées, et une gestion efficace des erreurs. Les fonctionnalités avancées, telles que la gestion asynchrone, la sécurité robuste avec l'implémentation de l'authentification et de l'autorisation, attestent de notre capacité à concevoir des solutions logicielles complètes et sécurisées.

De plus, notre parcours a été enrichi par l'application de méthodologies agiles, avec une approche Scrumasterisée intégrée à notre processus de développement. L'utilisation de GitHub a joué un rôle central dans cette démarche collaborative. Nous avons créé un environnement de travail dynamique en initiant des projets, en définissant des issues pour une gestion précise des tâches, et en facilitant les discussions au travers de pull requests. Cette approche itérative et collaborative a considérablement amélioré notre efficacité, permettant des itérations rapides, des ajustements en temps réel, et assurant une transparence totale dans le processus de développement. Les pratiques de merge soigneusement orchestrées ont consolidé notre compréhension des workflows Git, contribuant ainsi à la pérennité et à la qualité exceptionnelle de notre projet.

En résumé, notre chef-d'œuvre représente bien plus qu'une simple application ; il incarne notre engagement envers l'excellence du développement logiciel, mettant en lumière une expertise fraîchement acquise qui promet d'apporter une valeur ajoutée significative à tout projet futur.


### Prérequis
- C#
- .Net v8.0
- EntityFramework v8.0
- TailwindCss v3.4.0
- Sqlite en dev 
- Postgre en déploiement  


### Base de Données et Migrations
- Pour créer une migrations utiliser la cmd `dotnet ef migrations add choisirnomdelamigration`
- Mettre a jour la migrations créer juste au dessus faire : `dotnet ef database update`

### Structure du Projet
#### le projet c'est un site mettant en avant :

- Une page Home qui résume ce qu'est le restaurant et ce qu'il propose avec l'adresse et les horaires du restaurant

- Une page Carte qui représentes les différents plat proposer avec leurs prix

- Une page Reservation qui permet de réserver une table le nombre de couverts et une remarque spécifient ce que le client ne veux pas voir dans son plats



### Administration
En ce qui concerne l'administration on y accède via le chemin localhost/adminNomDeLaVue avec une connection requise 
avec un compte crée uniquement en bdd et aucunement avec un formulaire traditionnelle côté front
et on y retrouve : 
- Une page Carte qui permet de voir d'éditez et de supprimer les élement de la carte


- Une page Réservation qui permet de voir d'éditez et de supprimer les élement de la Réservation 








- MCD : 


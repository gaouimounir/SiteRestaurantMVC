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

### Configuration de l'Environnement de Développement
- Ouvrez un terminal :
- Naviguez vers le répertoire où vous souhaitez cloner le projet :
  Utilisez la commande cd pour naviguer vers le répertoire où vous souhaitez stocker le projet cloné.
  `cd chemin/vers/le/repertoire`
- Clonez le projet :
`git clone https://github.com/simplon-lille-csharp-dotnet/ShabbatBrunch.git`
Cette commande créera une copie locale du projet sur votre machine.
Accédez au répertoire du projet cloné :
Utilisez la commande cd pour entrer dans le répertoire du projet cloné.
`cd shabbatbrunch`

- Une fois que tout celà est fait rester dans le dossier du projet et faire un `dotnet restore` pour installer les dépendances nécéssaire
pour le projet
- Ensuite faire un `dotnet run watch`pour lancer le projet initial
- suivis d'un `npm run build` pour lancer le projet et la configuration de tailwindCss

### Base de Données et Migrations
- Pour créer une migrations utiliser la cmd `dotnet ef migrations add choisirnomdelamigration`
- Mettre a jour la migrations créer juste au dessus faire : `dotnet ef database update`

### Structure du Projet
#### le projet c'est un site mettant en avant :
- Une page Home qui résume ce qu'est le restaurant et ce qu'il propose avec l'adresse et les horaires du restaurant

<img src="ShabbatBrunch/wwwroot/img/imgreadme/home.png" width="500px">

- Une page Carte qui représentes les différents plat proposer avec leurs prix

<img src="ShabbatBrunch/wwwroot/img/imgreadme/carte.png" width="500px">

- Une page Reservation qui permet de réserver une table le nombre de couverts et une remarque spécifient ce que le client ne veux pas voir dans son plats

<img src="ShabbatBrunch/wwwroot/img/imgreadme/reserver.png" width="500px">

- Une page Avis qui permet d'ajouter un avis et une note 

<img src="ShabbatBrunch/wwwroot/img/imgreadme/posteavis.png" width="500px">


### Administration
En ce qui concerne l'administration on y accède via le chemin localhost/adminNomDeLaVue avec une connection requise 
avec un compte crée uniquement en bdd et aucunement avec un formulaire traditionnelle côté front
et on y retrouve : 
- Une page Carte qui permet de voir d'éditez et de supprimer les élement de la carte

<img src="ShabbatBrunch/wwwroot/img/imgreadme/carteAdmin.png" width="500px">

- Une page Réservation qui permet de voir d'éditez et de supprimer les élement de la Réservation 

<img src="ShabbatBrunch/wwwroot/img/imgreadme/reservationAdmin.png" width="500px">

- Une page NewsLetter qui permet de voir d'éditez et de supprimer les élement de la NewsLetter





### DDCU et MCD
- Diagrammes de cas d'utilisation :

<img src="ShabbatBrunch/wwwroot/img/imgreadme/ddcu_update.png" width="500px">





- MCD : 


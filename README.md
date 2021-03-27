# Crous' Ti Pizz'

### Auteurs :

- BERTRAND Pierre-Louis
- NEONAKIS Ionas
- TOULARHMINE Samir

# Rapport sur le travail fourni

## Instructions d'exécution

Ce projet a été réalisé avec le framework Xamarin et supporte les systèmes iOS et Android.

**Remarque :** L'application a été testée sous ces deux plateformes.

Afin de compiler ce projet, il suffit simplement d'importer la solution dans l'IDE Rider ou Visual Studio puis de compiler et d'exécuter le projet.

## Instruction d'utilisation

L'application affiche une page de connexion à la première ouverture. Il est ainsi possible de se connecter voire même de s'inscrire.

Après connexion, une vue avec plusieurs onglets s'affiche avec par défaut une vue cartographique de tous les restaurants disponibles dans le monde.
Il est possible d'en choisir un afin de commander dans ce dernier mais il est également possible de choisir ce même restaurant dans le deuxième onglet comportant la liste de tous ces restaurants triés dans l'ordre croissant de leur distance par rapport à l'utilisateur.

Une fois un restaurant choisi, il est possible de commander autant de pizzas que voulu à condition que cette dernière ne soit pas en rupture de stock.
Pour chaque pizza, il est possible d'en commander une quantité précise ou d'afficher les détails de cette dernière.
Il est également possible de filtrer les pizzas par leur nom.

Le panier est unique à chaque restaurant et est accessible depuis l'affichage des pizzas pour un restaurant. On peut supprimer un élément voire même vider complètement ce dernier.

L'application comporte également un onglet Commandes comportant les commandes passées.

Le dernier onglet est un profil comportant les informations de l'utilisateur modifiables à souhait. Cet onglet comporte également un bouton de déconnexion.

## Architecture du projet

Ce projet comprends trois sous-projets principaux :

- CroustiPizz.Mobile contient les sources communes aux deux plateformes supportées.
- CroustiPizz.Android contient les sources spécifiques à la plateforme Android
- CroustiPizz.iOS contient les sources spécifiques à la plateforme iOS

### Sources communes

Dans le sous-projet CroustiPizz.Mobile, on pourra y trouver plusieurs dossiers, voici une liste non exhaustive de leur signification :
- Controls contient tous les composants personnalisés
- Pages contient tout les vues de l'application
- Resources contient des polices ainsi que des fichiers d'internationalisation
- ViewModels contient un ViewModel pour chaque page

### Android 

Dans le sous-projet CroustiPizz.Android, on pourra y trouver quelques dossiers :
Les Customs Renderer se trouvent dans le dossier Classes.
Les couleurs principales se trouvent dans le fichier style.xml situé dans le dossier Resources. Ce dossier contient également toutes les images pour la plateforme.

### iOS

Dans le sous-projet CroustiPizz.iOS, on pourra y trouver également quelques dossiers :
Les Customs Renderer se trouvent dans le dossier Classes.
Dans Resources, on y trouvera les images de la plateforme.

## Difficultés rencontrées

Au cours du développement, nous avons pu rencontrer diverses difficultés que nous avons résolu en plus ou moins de temps. Certaines ont nécessité la sollicitation des enseignants pour éviter de perdre trop de temps.

Voici une listes non exhaustive des principales difficultés :

- La gestion de plusieurs BindingContexts dans l'affichage d'une liste était assez compliqué à comprendre à première vue.
- L'affichage de l'image pour chaque pizza s'est avéré plus compliqué de prévu mais s'est solutionné par l'ajout d'un attribut url dans l'objet Pizza.
- Pour gérer la quantité de chaque pizza, il nous a fallu faire hériter de NotifierBase la classe PizzItem.
- Le fait de devoir créer des CustomsRenderer et de gérer ces derniers sur chaques plateformes supportées.
- Le fait que les vues ne rendent pas pareil d'une plateforme à l'autre (comportements étranges...)

## Bug connus

N/A

## Conclusion

Ce projet nous a permis de comprendre les bases du développement multi-plateforme sous Xamarin. Il reste dommage que le peu de temps attribué à ce module nous a empêché d'approffondir certaines notions. Cette découverte a cependant permis d'envisager une carrière professionnelle dans ce domaine pour certains membres du groupe.

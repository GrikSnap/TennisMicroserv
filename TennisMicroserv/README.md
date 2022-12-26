# TennisMicroserv

## [HttpGet] /api/Player/players
Renvoi la liste des joueurs et les informations les concernants du meilleur au moin bon

## [HttpGet] /api/Player/{idPlayer}
Retourne les informations d'un joueur via son ID

## [HttpPost] /api/Player/statistique
Donne les statistiques suivantes :
- Le pay au ratio de partie gagnées le plus grand 
- L' IMC Moyen de tous les joueurs 
- La mediane de la taille des joueurs 

##Informations
- Les logs ce trouvent dans le dossier Logs
- Le fichier json source ce trouve dans le dossier Data

## Lancement

Pour un lancement en local (debug), il faut télécharger le projet puis le lancer a l'aide de VisualStudio 2022.
Le projet TennisMicroserv doit être choisie en tant que "projet de démarrage.

Dans le cas ou le lancement se ferait sur un environnement autre que "Developpement" https://www.postman.com/ permet d'en faciliter l'accès.


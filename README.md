# PhD Manager 2
**Projekt prešiel reštartom, nakoľko som nebol spokojný so stavom pôvodného projektu a reštart bol časovo menej náročný ako úprava pôvodného.**
Táto aplikácia je tvorená ako bakalárska práca. Výledkom bude informačný sytém pre správu doktorantského štúdia.

## Spustenie aplikácie
**Pre správne fungovanie je potrebné do User Secrets v projekte PhDManager pridať hodnoty zo súboru sample-secrets.json a prepísať Username a Password na Vaše meno a heslo do systému LDAP. V prípade testu prihlásenia cez Google je treba zmeniť aj ClientSecret a ClientId.**
Pre spustenie je potrebné mať nainštalovaný Docker. Projekt je potom spustiteľný buď cez Visual Studio alebo cez príkaz `docker compose up --build`.

## Prístup k viacerým funkciám
Po úspešnom prihlásení do aplikácie je potrebné v databáze (napr. cez program DBeaver zmeniť rolu uživateľa v tabuľke AspNetUserRoles na ID role admina). Po zmene je tento uživateľ definovaný ako **admin** a má prístup k všetkým funkciam aplikácie.
### Prístup admina:
1. Pridávanie, mazanie a úprava rolí uživateľov v položke Admin
2. Úprava informácií o užívateľoch
3. Pridávanie, mazanie a úprava dizertačných tém v položke Theses (Pre pridanie témy je nutné mať aspoň jedného uživateľa s rolou Teacher)
4. Mazanie a úprava komentárov pri nepotvrdených dizertačných prácach (Pre pridávanie komentárov je potrebné mať rolu Reviewer)
5. Potvrdzovanie dizertačných tém
6. Vytváranie, priradzovanie, úprava a mazanie študijného plánu.

## Momentálny stav aplikácie
V súčasnom stave aplikácia obsahuje:
1. Dva typy prihlasovania - prihlasovanie cez LDAP a prihlasovanie pre externistov
2. Registráciu pre externistov - externista sa môže zaregistrovať cez špeciálny link, ktorý je vygenerovaný adminom a má platnosť 1 hodinu (Pre testovacie účely sa link vygeneruje po kliknutí na položku Register)
3. Manažovanie užívateľov - admin dokáže manažovať užívateľov v grafickom prostredí
4. Pridávanie dizertačných tém - tému dokaže pridať admin alebo učiteľ
5. Úprava dizertačných tém - tému dokáže upravovať admin alebo učiteľ
6. Mazanie dizertačných tém - tému dokáže zmazať admin alebo učiteľ
7. Potvrdzovanie dizertačných tém - téma po pridaní nie je potvrdená a je nutné aby bola potvrdená adminom
8. Komentovanie dizertačných tém - téma, ktorá čaká na pridanie môže byť okomentovaná uživateľom s rolou Reviewer (Potrebné pre výhrady a možné úpravy k téme)
9. Mazanie komentárov dizertačných prác
10. Úprava komentárov dizertačných prác
11. Zobrazovanie potvrdených dizertačných tém - potvrdené témy si môže zobraziť ktokoľvek
12. Vytvorenie a priradenie štúdijného plánu študentovi - študentovi je možné priradiť dizertačnú tému do štúdijného plánu
13. Mazanie študijného plánu študenta
14. Úprava študijného plánu študenta

## Použité technológie
1. Aplikácia - Blazor Web App (Server)
2. Databáza - PostgreSQL

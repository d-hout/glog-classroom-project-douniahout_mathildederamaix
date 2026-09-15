public static class SeedData
{
    public static void Initialize()
    {
        using var db = new DataContext();

        //On regarde si la classe Oeuvres est vide, car c'est la classe centrale
        if (db.Oeuvres.Any())
        {
            return;
        }

        var auteur1 = new Auteurs { Nom = "Herbert", Prenom = "Frank" };
        var auteur2 = new Auteurs { Nom = "Nolan", Prenom = "Christopher" };
        db.Auteurs.AddRange(auteur1, auteur2);

        var oeuvre1 = new Oeuvres
        {
            Titre = "Dune",
            Type = OeuvreType.Livre,
            Auteurs = auteur1,
        };
        var oeuvre2 = new Oeuvres
        {
            Titre = "Interstellar",
            Type = OeuvreType.DVD,
            Auteurs = auteur2,
        };
        db.Oeuvres.AddRange(oeuvre1, oeuvre2);

        var adherent1 = new Adherents
        {
            Nom = "Dupont",
            Prenom = "Jean",
            Email = "jean.dupont@email.com",
        };
        var adherent2 = new Adherents
        {
            Nom = "Curie",
            Prenom = "Marie",
            Email = "marie.curie@email.com",
        };
        db.Adherents.AddRange(adherent1, adherent2);

        var copieDune = new Exemplaires { Oeuvre = oeuvre1, Statut = StatutExemplaire.Disponible };
        var copieInterstellar = new Exemplaires
        {
            Oeuvre = oeuvre2,
            Statut = StatutExemplaire.Disponible,
        };
        db.Exemplaires.AddRange(copieDune, copieInterstellar);
        db.SaveChanges();

        var emprunt = new Emprunts
        {
            Adherent = adherent2, // Marie Curie
            Exemplaire = copieDune,
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
            DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(11)),
            DateRetourReelle = null,
        };
        db.Emprunts.Add(emprunt);
        copieDune.Statut = StatutExemplaire.Emprunte;

        var reservation = new Reservations
        {
            Adherent = adherent1, //Jean Dupont
            Oeuvre = oeuvre1,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
        };
        db.Reservations.Add(reservation);

        db.SaveChanges();
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WSI.Goat.InternshipsContracts.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_ApplicationRoles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_ApplicationUsers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StageOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepresentativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageOrganizations_ApplicationUsers_RepresentativeId",
                        column: x => x.RepresentativeId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StageCoordinatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_ApplicationUsers_StageCoordinatorId",
                        column: x => x.StageCoordinatorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternshipContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternshipStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipStreetNumber = table.Column<int>(type: "int", nullable: false),
                    InternshipPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternshipOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternshipMentorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternshipAttendantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicalResearchLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InternshipCoordinatorRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskAnalysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MentorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisourSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MentorIsSigned = table.Column<bool>(type: "bit", nullable: false),
                    StudentIsSigned = table.Column<bool>(type: "bit", nullable: false),
                    SupervisourIsSigned = table.Column<bool>(type: "bit", nullable: false),
                    CoordinatorValidation = table.Column<bool>(type: "bit", nullable: false),
                    HasRiskAnalysis = table.Column<bool>(type: "bit", nullable: false),
                    NeedsMedical = table.Column<bool>(type: "bit", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternshipContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternshipContracts_ApplicationUsers_InternshipAttendantId",
                        column: x => x.InternshipAttendantId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipContracts_ApplicationUsers_InternshipMentorId",
                        column: x => x.InternshipMentorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipContracts_ApplicationUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipContracts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipContracts_InternshipTypes_InternshipTypeId",
                        column: x => x.InternshipTypeId,
                        principalTable: "InternshipTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternshipContracts_StageOrganizations_InternshipOrganizationId",
                        column: x => x.InternshipOrganizationId,
                        principalTable: "StageOrganizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticleInternshipContract",
                columns: table => new
                {
                    ArticlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InternshipContractsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleInternshipContract", x => new { x.ArticlesId, x.InternshipContractsId });
                    table.ForeignKey(
                        name: "FK_ArticleInternshipContract_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleInternshipContract_InternshipContracts_InternshipContractsId",
                        column: x => x.InternshipContractsId,
                        principalTable: "InternshipContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "BaseName", "Created", "Deleted", "Updated" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Student", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Mentor", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Supervisor", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Coordinator", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Admin", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("00000000-9999-9999-9999-000000000000"), "1.1 De stage vindt plaats binnen de periode van  tot en met  De Stagehandleiding vermeldt de exacte duur van het aantal stagedagen en/of -weken.\n                    1.2 In geval van afwezigheid van de Student wegen ziekte of overmacht kan de stageperiode verlengd worden. Deze verlenging dient schriftelijk te worden overeengekomen tussen Howest en de Stagegever, waarbij de nieuwe einddatum van de stage duidelijk wordt vastgelegd.\n                    1.3 De Stagegever aanvaardt dat de Student tijdens de stageperiode voor een aantal contactmomenten op Howest kan verwacht worden.", "Artikel 1 - Periode en duur" },
                    { new Guid("00000000-9999-9999-9999-000000000001"), "2.1 De stagegever bepaalt de werktijden in onderling overleg met de Student. Deze werktijden worden vastgesteld met inachtneming van de geldende arbeidsreglementering en, indien van toepassing, de sectorale bepalingen inzake arbeidsduur en arbeidsomstandigheden.\n                    2.2 Indien er tussen de Stagegever and de Student een arbeidsovereekomst lopende is, dan respecteert de Student-werknemer de werktijden van de Stagegever-werkgever zoals aagegeven in de arbeidsovereenkomst en/of het arbeidsreglement.\n                    2.3 De Stagegever voorziet een aangepaste werkplek op de stageplaats, zoals vermeld in de aanhef van deze Overeenkomst.\n                    2.4 Indien het adres van de stageplaats (ocasioneel) wijzigt (met inbegrip van telewerk), zal de Stagegever dit binnen een redelijke termijn vooraf schriftelijk melden aan de Student en aan de Stagebegeleider.\n                    In geval van (gedeeltelijk) telewerk meldt de Student schriftelijk aan zowel de Stagementor als de Stagebegeleider op welke locatie de stage-activiteiten bij telewerk uitgevoerd worden en hoe de Student bereikbaar is voor de Stagegever. Op her uurrooster wordt aangeduid wanneer telewerk toegepast wordt.\n                    Indien (een deel van) de stage wordt uitgevoerd op een locatie die niet onder de verantwoordelijkheid van de Stagegever valt, is hiervoor voorafgaand uitdrukkelijk en schriftelijk akkoord vereis van de Partijen.", "Artikel 2 - Werktijden en werkplaats" },
                    { new Guid("00000000-9999-9999-9999-000000000002"), "3.1 De Student zal zich richten op de vermelde taken in de omschrijving die als bijlage aan deze Overeenkomst toegevoegd wordt. De Student en de Stagementor zullen er gezamenlijk over waken dat de taken aansluiten bij de leerdoelen van de stage. Indien de Student vaststelt dat de toegewezen taken hiervan (dreigen te) afwijken, brendt de Student dit tijdig ter kennis van de Stagebegeleider.\n                    3.2 De Student verbindt zich ertoe zich volledig in te zetten voor de Stagegever en zijn belangen naar best vermogen te behartigen.\n                    3.3 De Stagegever verbindt zich ertoe de geldende arbeidsreglementering binnen de sector na te leven, voor zover deze van toepassing is op stagiaris.\n                    De nationale wetgeving omtrent welzijn, veiligheid en gezondheid op het werk is integraal van toepassing op de stage. De Stagegever past de bepalingen van de Welzijnswet van 4 augustus 1996 toe, evenals titel 4 van boek X van de Codex over het welzijn op het werk en alle andere relevante regelgeving. Overeenkomstig titel 4 in het boek X van de Codex over het welzijn op her werk neemt de Stagegever volgende verplichtingen in acht:\n                    - De Stagegever voert een voorafgaande analyse uit van de risico's waaran de Student kan worden blootgesteld en stelt de preventiemaatregelen vast die in acht moeten genomen worden. De resultaten vermelden in het bijzonder of een gezondheidstoezicht nodig is en, in voorkomend geval, de aard van de verplichte inetingen/onderzoeken.\n                    - De Stagegever bezorgt de resultaten van deze risicoanalyse ten minste voor de aanvang van de stage aan Howest, en bij voorkeur uiterlijk twee weken op voorhand, zodat eventueel noodzakelijk gezongheidstoezicht tijdig kan georganiseerd worden. De risicoanalyse wordt in bijlage gevoegd en maakt integraal deel uit van de Overeenkomst.\n                    - Wanneer uit de risicoanalyse blijkt dat de Student wordt tewerkgesteld in een activiteit waarvoor een type van gezondheidstoezicht van toepassing is, dan wordt het gezondheidstoezicht:\n                      1) ofwel uitgevoerd door de interne of externe dienst voor preventie en bescherming op het werk van de Stagegever. Bovendien onderwerpt de Stagegeven de Student, voorkomend geval, aan de inentingen/bijkomende onderzoeken;\n                      2) ofwel, enkel bij stagen van ten minste 30 kalendardagen en in opdracht van de Stagegever, uitgevoerd door de externe dienst voor preventie en bescherming op het werk van Howest. Bovendien onderwerpt Howest de Student, in voorkomend geval, aan de inentingen/bijkomende onderzoeken;\n                    - voor aanvang van de stage bewijst de Student ten aanzien van de Stagegever dat voldaan werk aan de gestelde eisen van de risicoanalyse.\n                    3.4 De persoonlijke beschermingsmiddelen die nodig zijn tijdens de uitvoering van de stage worden aan de Student kosteloos ter beschikking gesteld door de Stagegever.\n                    3.5 Indien er in het kader van de stage structureel contact (opvoeding, hulpverlening of begeleiding) met mindejarigen is, is de controle van een uittreksel uit het strafregister model 596.2 verplicht voor de ondertekening van de Stageovereenkomst. De uittreksel mag maximaal een maand oud zijn op het moment van voorlegging. De Student moet minstens onberispelijk gedrag vertonen en in de omgang met minderjarigen. Het uittreksel wordt door de Student zelf opgevraagd en bezorgd aan de Stagegever. Het niet voldoen aan deze vereiste kan aanleiding geven tot weigering of stopzetting van de stage.", "Artikel 3 - Taken en Verantwoordelijkheden" },
                    { new Guid("00000000-9999-9999-9999-000000000003"), "4.1 De Stagementor die door de Stagegever aangeduid wordt, zoals voornoemd in de aanhef van deze Overeenkomst, begeleidt de Student en ziet toe op her verloop van de stage.\n                    4.2 De Stagebegeleider die door Howest aangeduid wordt, volgt de stage op en legt daartoe de nodige contacten met de Stagementor.\n                    4.3 De evaluatie van de stage gebeurt op een objectieve manier. Howest is verantwoordelijk voor de eindbeoordeling van de stage and zal het eindcijfer bepalen. Daarbij houdt zij rekening met de beoordeling en/of het advies van de Stagementor. De evaluatie gebeurt op basis van de evaluatiecriteria zoals beschreven in de Stagehandleiding. De evaluatiecriteria en evaluatiemomenten worden bij de aanvang van de stage via Stagehandleiding gecomuniceerd.", "Artikel 4 - Begeleiding en evaluatie" },
                    { new Guid("00000000-9999-9999-9999-000000000004"), "5.1 Bij afwezigheid door ziekte of overmacht verwittigt de Student ogenblikkelijk zowel de Stagementor als de Stagebegeleider. Tevens bezorgt de Student binnen de vijf kalenderdagen een schriftelijk bewijs aan het Studentensecretariaat van Howest.\n                    5.2 Ekle afwezigheid waarvan geen kennis wordt gegeven door de Student, wordt door de Stagegever aan Howest gemeld.\n                    5.3 In geval van afwezigheid door ziekte of overmacht kan de stageperiode desgevallend verlengd worden overeenkomstig artikel 1 van deze Overeenkomst.", "Artikel 5 - Afwezigheden" },
                    { new Guid("00000000-9999-9999-9999-000000000005"), "6.1 De uitvoering van de stage is onbezoldigd.\n                    6.2 Een eventuele terugbetaling van woon-werkverkeer of andere onkosten is enkel mogelijk na voorafgaand schriftelijk akkoord van de Stagegever. Enkel onkosten gemaakt in het kader van de uitvoering van de stage en waarvoor de Student vooraf het schriftelijk akkoord verkregen heeft van de Stagegever, kunnen desgevallend door de Stagegever vergoed worden. De verdere regeling omtrent terugbetaling, inclusief het aanleveren van bewijsstukken, gebeurt in onderling overleg tussen de Student en de Stagegever. Deze onkostenvergoeding mag geen afbreuk doen aan het onbezoldig karakter van de stage.\n                    6.3 Tijdens de duur van de stage behoudt de Student het statuut van student aan Howest. Er ontstaat geen arbeidsovereenkomst tussen de Student en de Stagegever.\n                    6.4 Indien er tussen de Stagegever en de Student een arbeidsovereenkomst lopende is, dan schorst de Stageovereenkomst de arbeidsovereenkomst niet. De bezoldiging die in her kader van de arbeidsovereenkomst betaald wordt, doet geen afbreuk aan het onbezoldigd karakter van de Stageovereenkomst.", "Artikel 6 - Vergoeding en onkosten" },
                    { new Guid("00000000-9999-9999-9999-000000000006"), "7.1 De Partijen erkennen uitdrukkelijk de verplichting inzake beroepsgeheim, waarbij de betrokken Partijen gehouden zijn het beroepsgeheim, eigen aan de sector waarin de stage wordt volbracht, te eerbiedingen en zich te onthouden van enigerlei handeling die de goede naam of de bedrijvigheid van de Stagegever zou kunnen schaden, en dit zowel tijdens de stage als na afloop van de stage.\n                    7.2 De betrokken Partijen zijn verplicht tot geheimhouding van volgende informatie waarvan zij tijdens of naar aanleiding van de uitvoering van de stage kennisnemen: vertrouwelijke patiënten-, klanten- en personeelsgegevens, bedrijfskundige gegevens, financiële informatie, fabricage- en werkmethodes, therapie- en zorgschema's, strategische plannen, interne rapporten, software en technische informatie die behoren tot de werking of knowhow van de Stagegever. Daarnast geldt de geheimhoudingsplicht ook voor de informatie die door de Stagegever uitdrukkelijk en schriftelijk als vertrouwelijk aan de Student is meegedeeld.\n                    7.3 Vertrouwelijke informatie blijft te allen tijde de eingendom van de Stagegeven en mag uitsluitend worden gebruikt in het kader van de uitvoering van de stage. De student mag deze informatie niet delen met derden, behoudens voorafgaande schriftelijk toestemming van de Stagegever. Bij beëindiging van de stage verbindt de Student zich ertoe om op verzoek van de Stagegever alle vertrouwelijke documenten of gegevens(dragers), evenals andere informatie of middelen die uitdrukkelijk worden opgevraagd, ter vernietigen of terug te bezorgen.\n                    7.4 Een uitzondering op het verbod tot verspreiding geldt voor de Stagebegeleider van Howest en - in het geval van een eindwerk - de ledenn van de jury, voor zover deze op de hoogte moeten zijn van vertrouwelijke informatie in het kader van een adequate begeleiding of beoordeling van de Student. Howest verbindt zich ertoe dat zowel de Stagebegeleider als de juryleden gehouden zijn tot geheimhouding met betrekking tot deze informatie.\n                    7.5 Indien de Student vertrouwelijke informatie van de Stagegever wenst te verwerken in een verslag, presentatie of ander werk dat publiek wordt gedeeld of verdedigd, dient het ontwerpwerk minstens tien kalenderdagen voor de voorziene publicatie of indiening voorgelegd te worden aan de Stagementor.\n                    7.6 De Stagementor kan, als vertegenwoordiger van de Stagegever, binnen Microsoft kalenderdagen na ontvangst van het ontwerpwerk gemotiveerde bemerkingen formuleren of vragen om vertrouwelijke informatie te verwijderen. Indien er binnen die termijn geen bemerkingen worden geformuleerd, wordt het werkt geacht in zijn goedgekeurd. In geval van betwisting treedt de Stagebegeleider of Stagecoördinator van Howest op als bemiddelaar om tot een oplossing te komen.\n                    7.7 De Student stemt erin toe dat Howest en de Stagegever informatie uitwisselen voor de begeleiding en de evaluatie van de Student. De Stagegever verbindt zich tot een discretieplicht omtrent alle informatie die hij over de Student verneemt, en dit zowel tijdens de stage als na afloop van de stage.\n                    7.8 De geheimhoudingsverplichtingen opgenomen in dit artikel gelden tijdens de stagen en blijven gelden gedurende een periode van drie jaar na afloop van de stage, tenzij het vertrouwelijke karakter van de informatie op grond van de aard of context kennelijk een langere bescherming vereist. In dit laatste geval vervalt de geheimhouding wanneer de betreffende informatie openbaar is geworden of haar vertrouwelijk karakter heeft verloren.\n                    7.9 De Partijen, waaronder uitdrukkelijk begrepen de Stagegever, de Student en Howest (inclusief de Stagebegeleider og andere betrokken personeelsleden), erkennen dat de bepalingen in dit artikel gelden als een bindende regeling inzake vertrouwelijkheid en geheimhouding. Dit artikel heeft, wat betreft de geheimhoudingsverplichtingen, het juridische effect van een geheimhoudingsovereenkomst (NDA). Er zullen geen afzonderlijke NDA's ondertekend worden noch door de Student, noch door (personeelsleden van) Howest.", "Artikel 7 - Deontologie en geheimhouding" },
                    { new Guid("00000000-9999-9999-9999-000000000007"), "8.1 Verzekering lichamelijke ongevallen: gedurende de uitvoering van deze Overeenkomst is de Student verzekerd door Howest voor lichamelijke ongevallen tijdens de stage en op weg van en naar de stageplaats via de polis arbeidsongevallen of de schoolverzekering, binnen de waargorgen van deze polissen.\n                    8.2 Burgelijke aansprakelijkheid: overeenkomstig artikel II.355/1 van de Codex Hoger Onderwijks is de Stagegever aansprakelijk voor de schade die wordt veroorzaakt door een toevallige lichte schuld van de Student bij de uitvoering van de stage.\n                    De Student is persoonlijk aansprakelijk voor bedrog, zware schuld en lichte schuld wanneer die lichte schuld eerder gewoonlijk dan toevallig voorkomt.\n                    De Stagegever ziet erop toe dat de Student de opgedragen taken correct uitvoert zodat de Student eventuele fouten kan rechtzetten alvorens er schade ontstaat. Het is de verantwoordelijkheid van de Stagegever om zich desgevallend te verzekeren voor schadegevallen waarbij de Student betrokken kan zijn tijdens de uitvoering van de stage. Gebruik van voertuigen en schade aan materialen: voertuigen worden in geen enkel geval door Howest verzekerd.\n                    8.3 Ook schade aan materialen en voorwerpen, opgelopen tijdens een eventueel transport, wordt niet vezekerd door Howest.\n                    8.3.1 lichamelijke schade\n                    - Lichamelijke schade aan de student zelf ten gevolge van een ongeval tijdens het woon-werkverkeer, w.w.z. tijdens de verplaatsing van en naar de stageplaats, is verzekerd via de schoolverzekering van Howest. De lichamelijke schade aan de Student zelf ten gevolge van een ongeval tijdens de stage is verzekerd via de verzekering studentstage van Howest.\n                    - Lichamelijke schade aan een inzittende is verzekerd via de polis van het voertuig.\n                    - Bij een ongeval ligt de bewijslast steeds bij het slachtoffer. De Student of de inzittende zal moeten aantonen dat het ongeval gebeurd is van of naar de stage, of tijdens en door de deelname aan een activiteit in het kader van de stage in opdracht of met akkoord van de Stagegever.\n                    8.3.2 Materiële schade\n                    - Materiële schaden aan het voertuig (of materialen en voorwerpen die zich in het voertuig bevinden) van de Student tijdens de verplaatsing van en naar de stageplaats waarbij:\n                      -- De Student bij her ongeval in fout is: de verzekering van Howest kan dit nooit aangesproken worden. Enkel de verzekering van het voertuig van de Student wordt aangesproken voor de (materiële en lichamelijke) schade aan de tegenpartij. De schade aan het voertuig van de Student is voor de Student zelf.\n                      -- De tegenpartij bij het ongeval in fout is: de verzekering van Howest kan nooit aangesproken worden. De verzekering van de tegenpartij wordt aangesproken.\n                    - Materiële schade aan het voertuig (of materialen en voorwerpen die zich in het voertuig bevinden) van de Student tijdens een dienstverplaatsing in het kader van de stage (dit kan nooit in opdracht zijn van Howest):\n                      -- De Student moet voor het rijden met hun voertuig op dezelfde manier verzekerd worden als de medewerkers van de Stagegever. Dit moet op voorhand duidelijk en schriftelijk geregeld en gecommuniceerd worden door de Stagegever, waarbij het onder meer duidelijk moet zijn of de auto van de Student al dan niet omnium verzekerd is tijdens de verplaatsing en wie de eventuele franchise zal dragen. De Student mag niet verplicht worden met hun eigen voertuig te rijden. Dit kan geen beletsel zijn voor de uitvoering van de stage.\n                      -- De Student is bij her ongeval fout: de verzekering van Howest kan nooit aangesproken worden. De verzekering van het voertuig van de Student wordt aangesproken voor de (materiële en lichamelijke) schade aan de tegenpartij. De schade aan het voertuig van de Student wordt geregeld zoals op voorhand en schriftelijk overeengekomen met de Stagegever.\n                      -- De tegenpartij is bij het ongeval in fout: de verzekering van Howest kan nooit aangesproken worden. De verzekering van de tegenpartij wordt aangesproken.\n                    - Materiële schade aan een bedrijfsvoertuig:\n                      -- In het geval de Student tijdens hun stage met een bedrijfsvoertuig van de Stagegever rijdt, moet de Student voor het rijden met een bedrijfsvoertuig op dezelfde manier verzekerd worden als de medewerkers van de Stagegever. Dit moet op voorhand duidelijk en schriftelijk geregeld en gecommuniceerd worden door de Stagegever, waarbij onder meer duidelijk moet afgesproken worden wie de eventuele franchise zal dragen.\n                      -- De student is bij het ongeval in fout: de verzekering van Howest kan nooit aangesproken worden. De verzekering van het bedrijfsvoertuig wordt aangesproken voor de (materiële en lichamelijke) schade aan de tegenpartij. De schade aan het bedrijfsvoertuig wordt geregeld via de verzekering van het bedrijfsvoertuig en zoals op voorhand en schriftelijk overeengekomen met de Stagegever.\n                      -- De tegenpartij is bij het ongeval in fout: de verzekering van Howest kan nooit aangesproken worden. De verzekering van de tegenpartij wordt aangesproken.\n                    8.4 Toevertrouwde goederen: in de schoolverzekering is een dekking voor toevertrouwde goederen in het kader van stages opgenomen ter waarde van 75.000,00 Euro. De schade door diefstal, verduistering, bedrog of misbruik van vertrouwens is niet gedekt. Voor deze schade kan Howest niet aansprakelijk gesteld worden.\n                    8.5 Dekking onder voorbehoud: iedere dekking van schade door (de verzekeraar van) Howest is steeds onder voorbehoud van de aanvaarding door de verzekeraar op basis van de polisvoorwaarden en algemene voorwaarden.\n                    8.6 Aangifte: de Student meldt elk ongeval/schadegeval dat zich voordoet tijdens (de weg van of naar) de stage binnen de 24 uur:\n                    - Aan de verzekeringsdienst van Howest, via het online aangifteformulier op de Howest site : http://www.howest.be/aangifteongevallenstudenten;\n                    - Aan de Stagebegeleider van Howest;\n                    - Aan de Stagementor van de Stagegever.\n                    Alle documenten, attesten, enz. met betrekking tot verzekeringen, aangiftes van ongevallen, enz. zijn terug te vinden via http://www.howest.be/aangifteongevallenstudenten.\n                    8.7 Ernstige arbeidsongevallen: indien er zich een ernstig arbeidsongeval voordoet, onderzoekt de interne dienst voor preventie en bescherming van de Stagegever het ongeval og maakt een omstandig verslag op. De kosten voor dit onderzoek worden gedragen door de Stagegever.", "Artikel 8 - Verzekering en Aansprakelijkheid" },
                    { new Guid("00000000-9999-9999-9999-000000000008"), "9.1 Howest, al dan niet op verzoek van de Student, en de Stagegever kunnen de Stageovereenkomst eenzijdig opschorten en/of eenzijdig beëindigen in volgende gevallen:\n                    - Bij ernstige schending van de Stageovereenkomst;\n                    - In geval van een deontologische fout, manifeste onbekwaamheid, wangedrag, slechte wil, opzettelijke overtreding van deze Overeenkomst, alsook in geval van ongewettigde afwezigheid van de Student;\n                    - Wanneer de fysieke of psychische gezondheid van de Student in gevaar komt;\n                    - Wanneer Howest vaststelt dat de doelstellingen en criteria met betrekking tot de stage, zoals vastgelegd in de stageomschrijving en/of de Stagehandleiding, niet of onvoldoende bereikt worden.\n                    9.2 De eenzijdige beëindiging van de Stageovereenkomst door Howest of de Stagegever moet gebeuren via een gemotiveerd aangetekend schrijven aan de andere betrokken partijen.\n                    9.3 Er zal steeds gestreefd worden naar een gezamenlijke voorafgaande bespreking van de situatie in aanwezigheid van alle Partijen.\n                    9.4 Indien de stage definitief beëindigd wordt, zal er een document van stopzetting van de stage ondertekend worden door alle Partijen.\n                    9.5 In geval van overmacht aan de zijde van één van de Partijen zal de ene partij de andere Partijen onmiddellijk schriftelijk op de hoogte brengen onder vermelding van de oorzaak, de aard en te verwachten duur van de overmacht.\n                    In situaties van overmacht ten gevolge van aan deze Overeenkomst externe omstandigheden, kan de uitvoering van de Stageovereenkomst worden geschorst. Desgevallend en van zodra dit mogelijk is, zal met alle betrokken Partijen worden overlegd hoe en in welke mate, mits de nodige aanpassingen, de Stageovereenkomst verder kan uitgevoerd worden. Als uit het overleg evenwel blijkt dat de verdere uitvoering van de Stageovereenkomst niet meer mogelijk is, wordt deze in onderling akkoord beëindigd.\n                    9.6 Geen van de Partijen kan aansprakelijk gesteld worden voor het niet-uitvoeren van de verbintenissen, zoals gestipuleerd in onderhavige Overeenkomst.\n                    9.7 Indien er tussen de Stagegever en de Student een arbeidsovereenkomst lopende is, dan zal de eventuele vervroegde beëindiging van de Stageovereenkomst geen invloed hebben op de lopende arbeidsovereenkomst, en omgekeerd.", "Artikel 9 - Vroegtijdige beëindiging" },
                    { new Guid("00000000-9999-9999-9999-000000000009"), "Het uitvoeren van de stage vormt een middelenverbintenis, wat betekent dat de Student zich ertoe verbindt zich naar best vermogen in te zetten om de doelstellingen van de stage te behalen, zonder dat er een garantie is op een bepaald resultaat. Er is geen sprake van een resultaatsverbintenis.", "Artikel 10 - Middelenverbintenis" },
                    { new Guid("00000000-9999-9999-9999-000000000010"), "11.1 De Partijen komen overeen dat de persoonsgegevens die in het kader van deze Overeenkomst uitgewisseld worden, zullen verwerkt worden overeenkomstig de bepalingen van de Europese Verordening EU 2016/679 met betrekking tot de verwerking van persoonsgegevens (Algemene Verordening Gegevensbescherming) en deze na te leven, alsook de nationale toepasselijke wetgeving te respecteren. Meer bepaald komen de Partijen uitdrukkelijk overeen dat de persoonsgegevens:\n                    - voor welbepaalde, uitdrukkelijk omschreven en gerechtvaardigde doeleinden worden verzameld en vervolgens niet verder mogen verwerkt worden op een met die doeleinden onverenigbare wijze (‘doelbinding’);\n                    - beperkt worden tot wat noodzakelijk is voor de doeleinden waarvoor zij worden verwerkt (‘minimale gegevensverwerking’);\n                    - niet langer worden opgeslagen c.q. bewaard dan nodig is voor de doeleinden waarvoor de verwerking noodzakelijk is (‘opslagbeperking’);\n                    - vertrouwelijk worden behandeld.\n                    11.2 De Stagegever blijft te allen tijde eigenaar van, c.q. rechthebbende op de persoonsgegevens. De Stagegever garandeert de rechtmatigheid van de verwerking van de persoonsgegevens, inclusief de transparantie aan de betrokkenen, evenals de bescherming van de rechten van de betrokkenen overeenkomstig de geldende reglementering en de Algemene Verordening Gegevensverwerking.\n                    11.3 In het kader van de uitvoering van de stageactiviteiten, zal de Student de persoonsgegevens uitsluitend verwerken in opdracht og volgens de instructies van de Stagegever and zal de Student het gegevensbeschermingsbeleid en het privacybeleid van de Stagegever naleven.\n                    11.4 De Student mag de persoonsgegevens niet verwerken voor andere doeleinden dan in het kader van de stage of op een wijze die onverenigbaar is met deze Stageovereenkomst. De Student zal de persoonsgegevens als strikt vertrouwelijk behandelen en zal enkel die gegevens verwerken die strikt noodzakelijk zijn voor de uitvoering van de stage. De Student zal de persoonsgegevens niet aan derden meedelen, noch derden toegang geven tot de persoonsgegevens. De Student zal alle redelijke maatregelen treffen om de vertrouwelijkheid te verzekeren en ervoor zorgen dat de toegang tot deze gegevens steeds strikt beperkt wordt.", "Artikel 11 - Verwerking persoonsgegevens" },
                    { new Guid("00000000-9999-9999-9999-000000000011"), "12.1 Deze Overeenkomst wordt beheerst door het Belgisch recht.\n                    12.2 In geval er een geschil uit of met betrekking tot deze Overeenkomst mocht ontstaan tussen de Partijen, zullen de Partijen dit vooreerst in der minne trachten te regelen.\n                    12.3 Indien er geen minnelijke regeling kan bereikt worden, kunnen de Partijen het geschil aanhangig maken bij de bevoegde rechtbank van het gerechtelijk arrondissement West-Vlaanderen, afdeling Kortrijk, welke rechtbank exclusief territoriaal bevoegd zal zijn om te oordelen over de geschillen ontstaan naar aanleiding van deze Overeenkomst.", "Artikel 12 - Geschillenregeling" },
                    { new Guid("00000000-9999-9999-9999-000000000012"), "13.1 De Partijen mogen deze Overeenkomst niet aan derden overdragen zonder de voorafgaande schriftelijke toestemming van alle Partijen.\n                    13.2 Deze Stageovereenkomst geldt onverminderd de bepalingen van de Stagehandleiding en vormt, samen met deze Stagehandleiding, het enige geldig en bindend kader tussen de Partijen met betrekking tot de stage.\n                    13.3 Wijzigingen aan deze Overeenkomst kunnen enkel worden aangebracht met het schriftelijk akkoord van alle Partijen.\n                    13.4 De Partijen verklaren deze Overeenkomst, inclusief de bijlagen te kennen, een exemplaar te hebben ontvangen en deze te zullen naleven.\n                    13.5 Deze Overeenkomst kan digitaal ondertekend worden door alle Partijen. Een digitale handtekening heeft dezelfde juridische waarde als een handgeschreven handtekening. De Partijen komen overeen dat elektronische kopieën van deze Overeenkomst en de digitale handtekeningen volkomen rechtsgeldig zijn en voor alle doeleinden als origineel worden beschouwd.", "Artikel 13 - Overige bepalingen" }
                });

            migrationBuilder.InsertData(
                table: "InternshipTypes",
                columns: new[] { "Id", "BaseName", "Created", "Deleted", "Updated" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-3333-000000000001"), "Meewerkstage (meeloopstage)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("30000000-0000-0000-3333-000000000002"), "Afstudeerstage (onderzoeksstage)", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("30000000-0000-0000-3333-000000000003"), "Snuffelstage / observatiestage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("30000000-0000-0000-3333-000000000004"), "Maatschappelijke stage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("30000000-0000-0000-3333-000000000005"), "Werkstage / zomerstage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { new Guid("30000000-0000-0000-3333-000000000006"), "Buitenlandse stage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "ApplicationRoleId", "BaseName", "Created", "Deleted", "Email", "FirstName", "Function", "LastName", "Password", "Signature", "TelephoneNumber", "Updated" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000005"), "admin.admin", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@howest.be", "Admin", "Administrator", "Admin", "WW123", null, "/", null },
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000003"), "az.oostende", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "info@azoostende.be", "AZ", "Supervisor stageplaats", "Oostende", "WW123", null, "050 33 32 11", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000004"), "annelies.marechal", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "annelies.marechal@howest.be", "Annelies", "Stagecoördinator MW", "Marechal", "WW123", null, "0497 27 49 81", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000004"), "kim.maes", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "kim.maes@howest.be", "Kim", "Stagecoördinator TP", "Maes", "WW123", null, "+32 476 248434", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004"), "kristien.degroof", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "kristien.de.groof@howest.be", "Kristien", "Stagecoördinator Ortho", "De Groof", "WW123", null, "0474 72 98 63", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000004"), "ann.valcke", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ann.valcke@howest.be", "Ann", "Stagecoördinator SW", "Valcke", "WW123", null, "0478 75 79 22", null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000002"), "bart.soete", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "bart.soete@azoostende.be", "Bart", "Stagementor", "Soete", "WW123", null, "0455 11 22 33", null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000001"), "dylan.hourez", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "dylan.hourez@student.howest.be", "Dylan", "Student", "Hourez", "WW123", null, "0466 44 55 66", null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001"), "kahuan.ng", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "kuahan.ng@student.howest.be", "Kahuan", "Student", "Ng", "WW123", null, "0466 44 55 90", null },
                    { new Guid("10000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000002"), "mileto.dimarco", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "mileto.dimarco@azoostende.be", "Mileto", "Stagementor", "Di Marco", "WW123", null, "0455 11 22 11", null },
                    { new Guid("10000000-0000-0000-0000-000000001111"), new Guid("00000000-0000-0000-0000-000000000003"), "az.sint.lucas", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "info@azsintlucas.be", "AZ", "Supervisor stageplaats", "Sint Lucas", "WW123", null, "050 33 11 11", null }
                });

            migrationBuilder.InsertData(
                table: "Clusters",
                columns: new[] { "Id", "BaseName", "Created", "Deleted", "SupervisorId", "Updated" },
                values: new object[] { new Guid("00000000-0000-0000-1111-000000000001"), "Sociaal Agogisch Werk", new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("10000000-0000-0000-0000-000000001111"), null });

            migrationBuilder.InsertData(
                table: "StageOrganizations",
                columns: new[] { "Id", "BaseName", "Created", "Deleted", "Municipality", "Number", "OrganizationNumber", "PostalCode", "RepresentativeId", "Signature", "Street", "Updated" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "AZ Oostende", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "West-Vlaanderen", 10, "BE 0800.023.336", "8400", new Guid("10000000-0000-0000-0000-000000000001"), null, "Gouwelozestraat", null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "BaseName", "ClusterId", "Created", "Deleted", "Location", "StageCoordinatorId", "Updated" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000000"), "Maatschappelijk Werk", new Guid("00000000-0000-0000-1111-000000000001"), new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kortrijk", new Guid("10000000-0000-0000-0000-000000000002"), null },
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Toegepaste Psychologie", new Guid("00000000-0000-0000-1111-000000000001"), new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Brugge", new Guid("10000000-0000-0000-0000-000000000003"), null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Orthopedagogische Begeleiding", new Guid("00000000-0000-0000-1111-000000000001"), new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kortrijk", new Guid("10000000-0000-0000-0000-000000000004"), null },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Sociaal Werk", new Guid("00000000-0000-0000-1111-000000000001"), new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kortrijk", new Guid("10000000-0000-0000-0000-000000000005"), null }
                });

            migrationBuilder.InsertData(
                table: "InternshipContracts",
                columns: new[] { "Id", "BaseName", "CoordinatorValidation", "Created", "Deleted", "DepartmentId", "EndDate", "EndDate2", "HasRiskAnalysis", "InternshipAttendantId", "InternshipCoordinatorRemarks", "InternshipMentorId", "InternshipMunicipality", "InternshipOrganizationId", "InternshipPostalCode", "InternshipStreet", "InternshipStreetNumber", "InternshipTypeId", "MedicalResearchLocation", "MentorIsSigned", "MentorSignature", "NeedsMedical", "RiskAnalysis", "StartDate", "StartDate2", "StudentId", "StudentIsSigned", "StudentSignature", "SupervisourIsSigned", "SupervisourSignature", "Updated" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000011"), "Afstudeerstage Ortho - Kahuan Ng", false, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2027, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("10000000-0000-0000-0000-000000000004"), "Risicoanalyse is goedgekeurd door Kristien. Graag zo snel mogelijk de medische keuring inplannen.", new Guid("10000000-0000-0000-0000-000000000011"), "Oostende", new Guid("22222222-2222-2222-2222-222222222222"), "8400", "Gouwelozestraat", 10, new Guid("30000000-0000-0000-3333-000000000002"), null, false, null, true, null, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("10000000-0000-0000-0000-000000000008"), false, null, false, null, null },
                    { new Guid("40000000-0000-0000-0000-000000000012"), "Meewerkstage Leefgroep - Kahuan Ng", false, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("10000000-0000-0000-0000-000000000004"), null, new Guid("10000000-0000-0000-0000-000000000011"), "Oostende", new Guid("22222222-2222-2222-2222-222222222222"), "8400", "Gouwelozestraat", 10, new Guid("30000000-0000-0000-3333-000000000001"), null, false, null, true, null, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("10000000-0000-0000-0000-000000000008"), false, "uploads/archief_handtekening_kahuan.png", false, null, null },
                    { new Guid("40000000-0000-0000-0000-000000000013"), "Oriënterende Observatie Ortho - Kahuan Ng", false, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("20000000-0000-0000-0000-000000000002"), new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, new Guid("10000000-0000-0000-0000-000000000004"), "Dit betreft een korte observatiestage van twee weken. Mentor dient nog te worden toegewezen door de supervisor van AZ Oostende.", new Guid("10000000-0000-0000-0000-000000000011"), "Oostende", new Guid("22222222-2222-2222-2222-222222222222"), "8400", "Gouwelozestraat", 10, new Guid("30000000-0000-0000-3333-000000000003"), null, false, null, false, null, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("10000000-0000-0000-0000-000000000008"), false, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "ArticleInternshipContract",
                columns: new[] { "ArticlesId", "InternshipContractsId" },
                values: new object[,]
                {
                    { new Guid("00000000-9999-9999-9999-000000000000"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000000"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000000"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000001"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000001"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000001"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000002"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000002"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000002"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000003"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000003"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000003"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000004"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000004"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000004"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000005"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000005"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000005"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000006"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000006"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000006"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000007"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000007"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000007"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000008"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000008"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000008"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000009"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000009"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000009"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000010"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000010"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000010"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000011"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000011"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000011"), new Guid("40000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-9999-9999-9999-000000000012"), new Guid("40000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-9999-9999-9999-000000000012"), new Guid("40000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-9999-9999-9999-000000000012"), new Guid("40000000-0000-0000-0000-000000000013") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_ApplicationRoleId",
                table: "ApplicationUsers",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleInternshipContract_InternshipContractsId",
                table: "ArticleInternshipContract",
                column: "InternshipContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_SupervisorId",
                table: "Clusters",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ClusterId",
                table: "Departments",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_StageCoordinatorId",
                table: "Departments",
                column: "StageCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_DepartmentId",
                table: "InternshipContracts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_InternshipAttendantId",
                table: "InternshipContracts",
                column: "InternshipAttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_InternshipMentorId",
                table: "InternshipContracts",
                column: "InternshipMentorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_InternshipOrganizationId",
                table: "InternshipContracts",
                column: "InternshipOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_InternshipTypeId",
                table: "InternshipContracts",
                column: "InternshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InternshipContracts_StudentId",
                table: "InternshipContracts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StageOrganizations_RepresentativeId",
                table: "StageOrganizations",
                column: "RepresentativeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleInternshipContract");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "InternshipContracts");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "InternshipTypes");

            migrationBuilder.DropTable(
                name: "StageOrganizations");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");
        }
    }
}

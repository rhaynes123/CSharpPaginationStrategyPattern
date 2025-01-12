using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Physicists.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertIntoPhysicistsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = $@"INSERT INTO Physicists (Name, DateOfBirth) VALUES
('Albert Einstein', '1879-03-14'),
('Isaac Newton', '1643-01-04'),
('Marie Curie', '1867-11-07'),
('Nikola Tesla', '1856-07-10'),
('Galileo Galilei', '1564-02-15'),
('Richard Feynman', '1918-05-11'),
('Stephen Hawking', '1942-01-08'),
('Niels Bohr', '1885-10-07'),
('Erwin Schrödinger', '1887-08-12'),
('Max Planck', '1858-04-23'),
('James Clerk Maxwell', '1831-06-13'),
('Werner Heisenberg', '1901-12-05'),
('Paul Dirac', '1902-08-08'),
('Michael Faraday', '1791-09-22'),
('Enrico Fermi', '1901-09-29'),
('J.J. Thomson', '1856-12-18'),
('Robert Oppenheimer', '1904-04-22'),
('André-Marie Ampère', '1775-01-20'),
('Lise Meitner', '1878-11-07'),
('Henri Becquerel', '1852-12-15'),
('Dmitri Mendeleev', '1834-02-08'),
('William Thomson (Lord Kelvin)', '1824-06-26'),
('Christiaan Huygens', '1629-04-14'),
('Johannes Kepler', '1571-12-27'),
('Blaise Pascal', '1623-06-19'),
('Leonhard Euler', '1707-04-15'),
('Carl Friedrich Gauss', '1777-04-30'),
('Emmy Noether', '1882-03-23'),
('Hermann von Helmholtz', '1821-08-31'),
('Charles-Augustin de Coulomb', '1736-06-14'),
('Hans Christian Ørsted', '1777-08-14'),
('Augustin-Jean Fresnel', '1788-05-10'),
('Guglielmo Marconi', '1874-04-25'),
('Max Born', '1882-12-11'),
('Wolfgang Pauli', '1900-04-25'),
('Arthur Compton', '1892-09-10'),
('Alessandro Volta', '1745-02-18'),
('Pierre Curie', '1859-05-15'),
('Johannes Diderik van der Waals', '1837-11-23'),
('Otto Hahn', '1879-03-08'),
('Daniel Bernoulli', '1700-02-08'),
('Francis Hauksbee', '1660-05-27'),
('Jean-Baptiste Biot', '1774-04-21'),
('Thales of Miletus', '-624-01-01'), -- Approximate date
('Archimedes', '-287-01-01'), -- Approximate date
('Hipparchus', '-190-01-01'), -- Approximate date
('Alhazen (Ibn al-Haytham)', '965-07-01'),
('Aryabhata', '476-01-01'),
('Georg Ohm', '1789-03-16');
";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

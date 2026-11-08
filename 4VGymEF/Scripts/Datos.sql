USE [4vGym];
GO

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -- ATENCION: elimina todos los registros de estas cuatro tablas.
    DELETE FROM dbo.ActivityInstructor;
    DELETE FROM dbo.Activities;
    DELETE FROM dbo.Instructors;
    DELETE FROM dbo.ActivityTypes;

    -- IDs fijos para que las claves foraneas sean predecibles.
    SET IDENTITY_INSERT dbo.ActivityTypes ON;
    INSERT INTO dbo.ActivityTypes (Id, Name, NumberInstructors) VALUES
    (1, N'BodyPump', 2),
    (2, N'Spinning', 1),
    (3, N'Pilates', 1),
    (4, N'CrossFit', 3);
    SET IDENTITY_INSERT dbo.ActivityTypes OFF;

    SET IDENTITY_INSERT dbo.Instructors ON;
    INSERT INTO dbo.Instructors (Id, Name, Email, PhotoUrl, Phone) VALUES
    (1, N'Ana García', 'ana@example.com', 'https://example.com/ana.jpg', '600123456'),
    (2, N'Markel López', 'markel@example.com', 'https://example.com/markel.jpg', '611234567'),
    (3, N'Laura Martínez', 'laura@example.com', 'https://example.com/laura.jpg', '622345678'),
    (4, N'Ander Ruiz', 'ander@example.com', 'https://example.com/ander.jpg', '633456789'),
    (5, N'Marta Pérez', 'marta@example.com', 'https://example.com/marta.jpg', '644567890'),
    (6, N'David Sánchez', 'david@example.com', 'https://example.com/david.jpg', '655678901');
    SET IDENTITY_INSERT dbo.Instructors OFF;

    SET IDENTITY_INSERT dbo.Activities ON;
    INSERT INTO dbo.Activities (Id, ActivityTypeId, MaxParticipants, DateStart, DateEnd) VALUES
    (1, 1, 20, '2026-11-09T09:00:00', '2026-11-09T10:00:00'),
    (2, 2, 15, '2026-11-09T09:30:00', '2026-11-09T10:30:00'),
    (3, 3, 12, '2026-11-09T10:00:00', '2026-11-09T11:00:00'),
    (4, 4, 16, '2026-11-09T11:00:00', '2026-11-09T12:00:00'),
    (5, 1, 20, '2026-11-10T09:00:00', '2026-11-10T10:00:00'),
    (6, 2, 15, '2026-11-10T09:00:00', '2026-11-10T10:00:00'),
    (7, 3, 12, '2026-11-10T17:00:00', '2026-11-10T18:00:00'),
    (8, 4, 16, '2026-11-11T18:00:00', '2026-11-11T19:00:00');
    SET IDENTITY_INSERT dbo.Activities OFF;

    INSERT INTO dbo.ActivityInstructor (ActividadesId, ListaInstructoresId) VALUES
    (1, 1), (1, 2),
    (2, 3),
    (3, 1),
    (4, 2), (4, 4), (4, 5),
    (5, 1), (5, 3),
    (6, 2),
    (7, 4),
    (8, 1), (8, 5), (8, 6);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

-- Comprobacion
SELECT * FROM dbo.ActivityTypes ORDER BY Id;
SELECT * FROM dbo.Instructors ORDER BY Id;
SELECT * FROM dbo.Activities ORDER BY Id;
SELECT * FROM dbo.ActivityInstructor ORDER BY ActividadesId, ListaInstructoresId;
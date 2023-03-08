CREATE TABLE `User` (
  `Id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `FullName` VARCHAR(75) NOT NULL,
  `Email` VARCHAR(50) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `DateBirth` datetime NOT NULL ,
  `LastModifiedDate` datetime NOT NULL ,
  `CreatedDate` datetime NOT NULL 
);


CREATE TABLE `Survey` (
  `Id` INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `Title` VARCHAR(75) NOT NULL,
  `CanUnregistredVote` BOOL NOT NULL,
  `CanUserUpdateVote` BOOL NOT NULL,
  `OptionsSelectedCount` INT NOT NULL ,
  `FKCreatorUser` INT NOT NULL ,
  `VotesCount` INT NOT NULL ,
  `FinishDate` datetime NOT NULL ,
  `LastModifiedDate` datetime NOT NULL ,
  `CreatedDate` datetime NOT NULL 
);
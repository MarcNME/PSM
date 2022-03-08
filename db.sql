-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema PSM
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema PSM
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `PSM` DEFAULT CHARACTER SET utf8 ;
USE `PSM` ;

-- -----------------------------------------------------
-- Table `PSM`.`tblFamilyMembers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblFamilyMembers` (
  `memberID` INT NOT NULL,
  `relation` ENUM('f', 'm', 's', 't') NULL,
  `description` VARCHAR(45) NULL,
  `contribution` INT NULL,
  `firstName` VARCHAR(45) NULL,
  PRIMARY KEY (`memberID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblEmployees`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblEmployees` (
  `employeeID` INT NOT NULL,
  `firstName` VARCHAR(45) NULL,
  `lastName` VARCHAR(45) NULL,
  `gender` ENUM('m', 'w') NULL,
  `birthday` DATE NULL,
  `locationID` INT NULL,
  `adress` VARCHAR(45) NULL,
  `phoneNumber` VARCHAR(45) NULL,
  `entryDate` DATE NULL,
  `minSalary` INT NULL,
  `departmentID` INT NULL,
  `departmentHeadName` VARCHAR(45) NULL,
  `departmentHeadLastName` VARCHAR(45) NULL,
  `familyMemberID` INT NULL,
  PRIMARY KEY (`employeeID`),
  INDEX `departmentID_idx` (`departmentID` ASC) ,
  INDEX `familyMemberID_idx` (`familyMemberID` ASC) ,
  CONSTRAINT `fk_departmentID`
    FOREIGN KEY (`departmentID`)
    REFERENCES `PSM`.`tblDepartmens` (`departmentID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_familyMemberID`
    FOREIGN KEY (`familyMemberID`)
    REFERENCES `PSM`.`tblFamilyMembers` (`memberID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblDepartmens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblDepartmens` (
  `departmentID` INT NOT NULL AUTO_INCREMENT,
  `departmentName` VARCHAR(45) NULL,
  `departmentHeadName` VARCHAR(45) NULL,
  `departmentHeadLastName` VARCHAR(45) NULL,
  `userID` INT NOT NULL,
  PRIMARY KEY (`departmentID`),
  INDEX `userID_idx` (`userID` ASC) ,
  CONSTRAINT `fk_userID`
    FOREIGN KEY (`userID`)
    REFERENCES `PSM`.`tblEmployees` (`employeeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblOrders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblOrders` (
  `orderID` INT NOT NULL,
  `value` INT NULL,
  `payDate` DATE NULL,
  `customer` VARCHAR(45) NULL,
  PRIMARY KEY (`orderID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblActivity`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblActivity` (
  `activityID` INT NOT NULL,
  `description` VARCHAR(45) NULL,
  `price` INT NULL,
  PRIMARY KEY (`activityID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblReports`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblReports` (
  `reportID` INT NOT NULL,
  `Date` DATE NULL,
  `orderID` INT NULL,
  `activityID` INT NULL,
  `hours` INT NULL,
  `employeeID` INT NULL,
  PRIMARY KEY (`reportID`),
  INDEX `orderID_idx` (`orderID` ASC) ,
  INDEX `taskID_idx` (`activityID` ASC) ,
  INDEX `employeeID_idx` (`employeeID` ASC) ,
  CONSTRAINT `fk_orderID_reports`
    FOREIGN KEY (`orderID`)
    REFERENCES `PSM`.`tblOrders` (`orderID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_activityID_Reports`
    FOREIGN KEY (`activityID`)
    REFERENCES `PSM`.`tblActivity` (`activityID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_employeeID`
    FOREIGN KEY (`employeeID`)
    REFERENCES `PSM`.`tblEmployees` (`employeeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `PSM`.`tblProofOfActivity`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `PSM`.`tblProofOfActivity` (
  `proofID` INT NOT NULL,
  `firstName` VARCHAR(45) NULL,
  `lastName` VARCHAR(45) NULL,
  `date` DATE NULL,
  `orderID` INT NULL,
  `activityID` INT NULL,
  `hours` INT NULL,
  PRIMARY KEY (`proofID`),
  INDEX `taskID_idx` (`activityID` ASC) ,
  INDEX `orderID_idx` (`orderID` ASC) ,
  CONSTRAINT `fk_activityID_proofOfActivity`
    FOREIGN KEY (`activityID`)
    REFERENCES `PSM`.`tblActivity` (`activityID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_orderID_fk_activityID_proofOfActivity`
    FOREIGN KEY (`orderID`)
    REFERENCES `PSM`.`tblOrders` (`orderID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

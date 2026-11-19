# Volunteer Management System & Data Quality Dashboard

A portfolio project demonstrating the complete software lifecycle, from requirements analysis and system design through application development, testing, support operations, and data quality management.

## Project Overview

The Volunteer Management System (VMS) is a web-based application designed to manage volunteers, events, attendance tracking, and data quality monitoring.

The application was created as a portfolio project to showcase skills in:

- Application Support
- Systems Analysis
- Business Analysis
- Data Quality Management
- ASP.NET Development
- SQL Server Database Design
- Software Testing
- Incident Management
- Technical Documentation

---

## Business Problem

Many volunteer organizations rely on spreadsheets, email communication, and paper-based processes to manage volunteers and events.

These methods often result in:

- Duplicate records
- Missing contact information
- Reporting inaccuracies
- Manual administrative effort
- Poor data quality
- Limited visibility into operational metrics

The Volunteer Management System addresses these challenges through centralized data management and automated quality monitoring.

---

## Features

### Volunteer Management

- Create volunteers
- Update volunteer records
- View volunteer information
- Delete volunteer records

### Data Quality Dashboard

- Detect duplicate email addresses
- Identify missing email addresses
- Identify invalid phone numbers
- Calculate overall data quality score
- Monitor data quality trends

### Operational Dashboard

- Total volunteer count
- Data quality score
- Open data quality issues
- Quality assessment indicators

---

## Technology Stack

### Frontend

- ASP.NET Core MVC
- Razor Views
- Bootstrap 5

### Backend

- C#
- ASP.NET Core

### Database

- SQL Server LocalDB
- Entity Framework Core

### Development Tools

- Visual Studio 2026 Community
- SQL Server Management Studio (SSMS)
- Git
- GitHub

---

## Solution Architecture

```text
Browser
    |
    v
ASP.NET MVC
    |
    v
Entity Framework Core
    |
    v
SQL Server
```

---

## Current Application Structure

```text
VolunteerManagementSystem

├── Controllers
│   ├── HomeController
│   ├── VolunteersController
│   └── DataQualityController
│
├── Models
│   └── Volunteer
│
├── ViewModels
│   ├── DashboardViewModel
│   └── DataQualityDashboardViewModel
│
├── Data
│   └── VolunteerManagementContext
│
├── Views
│   ├── Home
│   ├── Volunteers
│   └── DataQuality
│
└── SQL Server Database
```

---

## Data Quality Metrics

The application currently measures:

### Completeness

Measures missing required information.

Examples:

- Missing email addresses
- Missing phone numbers

### Validity

Measures whether values meet defined business rules.

Examples:

- Phone numbers below minimum length
- Invalid email formats

### Uniqueness

Measures duplicate records.

Examples:

- Duplicate email addresses
- Duplicate volunteer profiles

---

## Dashboard Metrics

The dashboard calculates and displays:

- Total Volunteers
- Duplicate Emails
- Missing Emails
- Invalid Phone Numbers
- Open Data Quality Issues
- Data Quality Score

---

## Future Enhancements

### Event Management

- Create Events
- Edit Events
- Event Capacity Tracking

### Volunteer Registration

- Register Volunteers for Events
- Wait List Management
- Registration Status Tracking

### Attendance Tracking

- Event Attendance
- Volunteer Hours Tracking
- Participation Reporting

### Application Support Features

- Incident Dashboard
- Failed Login Monitoring
- System Health Dashboard
- Audit Logging

### Data Quality Enhancements

- Data Profiling Reports
- Duplicate Detection Engine
- Automated Quality Rules
- Data Steward Workflow

---

## Documentation

This project includes extensive portfolio documentation demonstrating software lifecycle artifacts.

### Requirements Analysis

- Business Requirements Document (BRD)
- User Stories
- Process Flows
- Requirements Traceability Matrix

### Testing

- Test Cases
- Bug Reports
- Test Summary Reports

### Operational Support

- Incident Reports
- User Lockout Runbook
- API Troubleshooting Guide
- Database Failure Runbook
- Incident Severity Matrix

### System Design

- Entity Relationship Diagram (ERD)
- System Architecture
- Data Flow Documentation

---

## Skills Demonstrated

### Application Support

- Incident Management
- Root Cause Analysis
- Troubleshooting
- Production Support
- Runbook Development

### Systems Analysis

- Requirements Analysis
- Process Modeling
- Data Modeling
- Solution Design

### Development

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Bootstrap

### Data Quality

- Data Profiling
- Data Validation
- Duplicate Detection
- Quality Metrics
- Information Quality Management

---

## Portfolio Disclaimer

This application was created as a professional portfolio project to demonstrate software engineering, application support, systems analysis, and data quality management skills.

The application, documentation, incident reports, test cases, and supporting artifacts are fictional and do not contain proprietary or confidential information from any employer, client, or organization.

---

## Author

Richard Broyles

MS Information Quality  
BS Information Science

Areas of Interest:

- Application Support Engineering
- Systems Analysis
- Data Governance
- Data Quality Management
- Software Development

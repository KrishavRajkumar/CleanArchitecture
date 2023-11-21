# Question

  The project `LegacyService` is a library written long time ago. Assuming the logic is perfectly **sound**, you are asked to refactor and modernise the code base, as well as improve its performance if possible.

## Tasks

### Complete the test project

- It is important to note that the `LegacyService` is not test covered, thus it is essential to complete the tests to ensure refactoring working.

### Refactoring by applying clean code principles e.g. SOLID and applicable design patterns

- Ideally async/await pattern should be applied;
- You can also leave your thoughts under **[Candidate-Comments](#Candidate-Comments)**;

## Restrictions

- `CandidateDataAccess` class and its `AddCandidate` method need to stay **static**
- Feel free to use you preferred Mock/Assertion libraries in the test projects

## Candidate Comments

Thanks for giving me this opportunity.

Assumed I am a senior dev in Infotrack and considered future scope of our project, I have restructured the code base with all latest stacks

1. Refactored the entire solution as needed. Used the following tech stacks
  - ASP.NET Core.
  - EF Core
  - Clean architecture
  - Automapper
  - xunit
  - MoQ
  - CQRS with MediateR and Repository
  - Swagger has been added to show the endpoints and models
2. Performance will be improved due to the latest tech stacks used
3. Future development activities will be streamlined and it is easy for new joinees and devs. 
   Also, Productivity will be increased in the team
4. All SOLID principles are used across this project
5. Created separate test projects for Controllers, Handlers and DBContexts
7. Logging mechanism added (Serilog)

Test cases:
==============

As we have refactored the solution, added the test cases ensure existing functionalities remains the same.
 - When_Candidate_Is_SecuritySpecialist_and_Credit_Score_Is_Lessthan_500_then_call_should_be_success  Candidate should be added successfully
 - When_Candidate_Is_SecuritySpecialist_and_Credit_Score_Is_GreaterThan_500_then_call_should_be_failed with credit check
 - When_Candidate_Is_FeautureDeveloper_and_Credit_Score_Is_Lessthan_500_then_call_should_be_failed with credit check
 - When_Candidate_Is_FeatureDeveloper_and_Credit_Score_Is_Greaterthan_500_then_call_should_be_success - Candidate should be added successfully
 - When_Candidate_Is_Not_In_The_Specified_Position_and_Credit_Score_DoesNot_Needed_then_call_should_be_success - Candidate should be added successfully
 - When_Candidate_Entered_incorrect_email_format_validation_error_should_be_thrown  - validation error messages
 - When_Candidate_Is_Less_Than_18Years_Old_Validation_error_should_be_thrown

DB Context test cases:
======================
As we have introduced, it is better to add tests around the ontext class. I have added few test cases and which can be further extended

Controller test classes:
========================
Controller test cases also introduced. It can be extended. In interest of time, just added one. 

Note:
==========
- In the existing solution, uspGetPositionById has been used in Position repository. Just replaced with EF Core functionality. 
  If we want to use the StoredProc, still we can use EF core feature for SPs.
- Also, I have not kept the Static class as requested in the readme doc. This has been replaced with all EF features. 
- Returning GetCredit value as 1000 from the WCF. There is a scope to improve this area. As I am not seeing the WCF service contract, just added the wrapper 

Running the project:
====================
- Code and test cases can be executed with the real time data without any issues.
- Please use VS2022.
- Once run, Please use Position end point to insert the position details 
- Then use Candidate post method to insert the candidate details
- GetAll or GetByID methods of Candidate endpoint can be used to retrieve the candidate details
- Test cases are used to cater all the scenarios presented in the existing solution

I would like to get an opportunity to walk-through the code to clarify any gaps.

Thanks,
Rajkumar
```diff
@@ Requirements Document @@
+1 Introduction
```
The introduction provides context for the project. Identify the software product(s) to be produced by name. Explain what the software product(s) will, and, if necessary, will not do.
%NAME is a brand-new social activity service used to stay socially active you know. 
The user is given the ability:
  -to create and maintain a profile describing their current interests and other personal data.
  -to look through other users profiles and mark them as based and cringe
  -to start a chat and keep messaging each other when mutual simpathy is achieved
```diff
+2 User Requirements
-2.1 Software Interfaces
```
http communication protocol and some hosting service maybe
```diff
-2.2 User Interfaces
```
Interaction is made through a web/android client containing registration page + chat/profile feed/profile managing pages.
```diff
-2.3 User Characteristics
```
The service is meant to be used by every human able to hold and use their mobile device and does not have active major mental issues intereested in finding a person to
  hang out with, visit some social event or do something else (under law restrictions).
```diff
-2.4 Assumptions and Dependencies
```
System requirements stay the same no matter what happens actually
```diff
+3 System Requirements
-3.1 Functional Requirements
```
The service is required to provide the possibility to create and manage a profile freely enough to express one's special capabilities and interests,
  to view geographically (or by other order) close profiles and express one's desire for communication and proceed if it's mutual.
```diff
-3.2 Non-Functional Requirements
```
The service is required to feature human design and meet functional requirements with no objectively bad desing solutions.
```diff
-3.3 SOFTWARE QUALITY ATTRIBUTES
```
Reliability and sequrity are most important quality attributes of the service. It should manage personal data according to user license statements.
User personal info can be only edited by this very user so the API can not be abused to violate this rule. The system also should be reliable and 
  work right and fast under heavy traffic loads.

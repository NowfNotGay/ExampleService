────────────────────────────────────────────────────────
📄 DOCUMENT HEADER – RULE MANIFEST
────────────────────────────────────────────────────────

Document Name : Engineering Rules & Coding Convention
Document Type : Internal Engineering Standard
Status : Active
Version : 1.0.0
Created Date : 2026-02-02
Last Updated : 2026-02-02

────────────────────────────────────────────────────────
🎯 Purpose
────────────────────────────────────────────────────────
This document defines the official engineering rules applied
across the system to ensure consistency, readability, and
long-term maintainability.

The rules act as a **contract**, not personal preference.

────────────────────────────────────────────────────────
🧩 Scope
────────────────────────────────────────────────────────
This rule set applies to:
• Backend services
• Application & domain logic
• Database interaction
• Error handling & exception strategy
• Naming & structure across layers

Frontend-specific rules are excluded unless explicitly stated.

────────────────────────────────────────────────────────
🛠️ Technology Context
────────────────────────────────────────────────────────
Primary Platform : .NET 5+
Database : SQL Server
Architecture Style : Layered / Domain-driven (lightweight)
Communication : REST / Internal APIs

This document is technology-aware but not technology-bound.

────────────────────────────────────────────────────────
📐 Rule Categories Included
────────────────────────────────────────────────────────
• General coding principles
• Naming conventions
• Structure & layering rules
• Error codes & exception handling
• Validation strategy
• Duplication & abstraction rules
• Code review & acceptance criteria
• Automation & AI-assisted review guidance

────────────────────────────────────────────────────────
📜 Rule Enforcement
────────────────────────────────────────────────────────
• Rules are enforced during code review
• Exceptions must have a clear business reason
• Temporary exceptions must be documented

"No business reason" = "Not accepted"

────────────────────────────────────────────────────────
🧠 Ownership & Change Policy
────────────────────────────────────────────────────────
Owner : Engineering Team
Change Proposal : Via pull request / internal review
Breaking Change : Requires team alignment

This is a living document and evolves with the system.

────────────────────────────────────────────────────────
END – DOCUMENT HEADER
────────────────────────────────────────────────────────

# 📘 Coding Convention – Error Handling & Naming Strategy

────────────────────────────────────────────────────────

## 🎯 Scope

This document defines:
• Error handling philosophy
• Error code structure
• Naming conventions for errors, actions, results
• How systems, developers, and users react to errors

Technology-agnostic.
Applicable to .NET, SQL Server, and any layered architecture.

────────────────────────────────────────────────────────

## 🧠 Error Handling Philosophy

• Error codes are **contracts**, not messages  
• Messages are for humans, codes are for systems  
• Errors exist to decide **what happens next**, not just to describe failure

Error codes drive:
• ⚙️ System action (retry, rollback, fallback)
• 🧑‍💻 Developer action (fix logic, adjust flow)
• 👤 User action (retry, correct input, contact support)

────────────────────────────────────────────────────────

## 🔢 Error Code Structure

### 📐 Format

x0xx

• x → Business domain identifier  
• 0 → Separator (fixed, reserved)  
• xx → Sequential number within the domain

────────────────────────────────────────────────────────

### 📌 Example

10001 – Invoice_NotFound

• Business domain: 10 (Invoice)  
• Sequence: 001

Meaning:
→ The invoice does not exist or cannot be accessed in the current business context

────────────────────────────────────────────────────────

## 🏷️ Business Domain Prefix

• 10xxx → Invoice  
• 11xxx → Payment  
• 12xxx → User / Account  
• 13xxx → Meeting / Voting  
• 90xxx → Infrastructure / Integration  
• 99xxx → Unknown / Unclassified

────────────────────────────────────────────────────────

## 🧾 Error Categories & HTTP Mapping

### Business Error

• Violates business rules
• Expected and recoverable

→ HTTP 400 / 409

Examples:
• Invoice already paid
• Voting period ended

────────────────────────────────────────────────────────

### Validation Error

• Invalid input
• Missing or malformed data

→ HTTP 400

Examples:
• Required field missing
• Invalid format

────────────────────────────────────────────────────────

### System Error

• Infrastructure or runtime failure
• Not caused by business logic

→ HTTP 500 / 503

Examples:
• Database unavailable
• External service timeout

────────────────────────────────────────────────────────

## 🏷️ Naming Convention (Core Rule)

### 📐 Naming Format

{Business}\_{Action}

• Business → Domain noun (Invoice, Payment, Vote)
• Action → Business verb (Create, Approve, Reject)

Names describe **business intent**, not technical steps.

────────────────────────────────────────────────────────

## ✍️ Naming Examples

### Commands

• Invoice_Create
• Invoice_Approve
• Vote_Cast
• Payment_Refund

────────────────────────────────────────────────────────

### Queries

• Invoice_GetById
• Invoice_ListByCustomer
• Vote_GetResult

────────────────────────────────────────────────────────

### Results / DTOs

• Invoice_GetById_Result
• Vote_Cast_Result
• Payment_Refund_Result

────────────────────────────────────────────────────────

### Errors

• Invoice_NotFound
• Invoice_AlreadyPaid
• Vote_PeriodClosed
• Payment_InsufficientBalance

────────────────────────────────────────────────────────

## 🚫 Naming Anti-Patterns

• Do NOT include technical words:
❌ HandleInvoice
❌ ProcessPayment
❌ ExecuteVote

• Do NOT include UI or transport concepts:
❌ InvoiceApi_Create
❌ CreateInvoiceRequest

• Do NOT encode workflow steps in names:
❌ Invoice_CheckThenCreate

────────────────────────────────────────────────────────

## 🎨 Frontend Naming

• No FE-specific naming convention
• Frontend **inherits entity-based names**
• FE reflects business actions
• FE does NOT redefine business meaning

Example:
• API → Invoice_GetById
• FE → useInvoiceGetById()

────────────────────────────────────────────────────────

## 🌐 API Error Response Contract

{
"errorCode": "10001",
"errorName": "Invoice_NotFound",
"errorMessage": "Invoice not found",
"traceId": "c2a1f8d9..."
}

• errorCode → Machine-readable, stable
• errorName → Business-meaningful identifier
• errorMessage → Human-readable, localizable
• traceId → Debug & log correlation

────────────────────────────────────────────────────────

## 🧩 Handling Flow

• Validate input at API boundary
• Handle business rules in application layer
• Throw system exceptions only for real failures
• Map all errors in one centralized place
• Always log with traceId

────────────────────────────────────────────────────────

## ⚖️ Trade-offs (Acknowledged)

• Higher communication cost initially
• Requires reading existing code before adding new logic

────────────────────────────────────────────────────────

## 💡 Long-term Gains

• Fewer duplicate functions
• Clearer business flow
• Easier code review
• Faster onboarding
• Less “why does this exist?” moments

────────────────────────────────────────────────────────

## 🏁 Final Note

Convention exists to **reduce cognitive load**,  
not to enforce dogma.

If a rule does not improve understanding,  
**we question the rule**.

────────────────────────────────────────────────────────
END – Coding Convention

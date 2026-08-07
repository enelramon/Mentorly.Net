using Mentorly.Application.Abstractions.Persistence;
using Mentorly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mentorly.Infrastructure.Persistence.Repositories;

public sealed class PeerReviewRepository(MentorlyDbContext dbContext) : IPeerReviewRepository
{
    public Task<PeerReview[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .AsNoTracking()
            .OrderByDescending(review => review.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public Task<PeerReview?> GetByIdAsync(
        Guid peerReviewId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .FirstOrDefaultAsync(review => review.Id == peerReviewId, cancellationToken);
    }

    public Task<PeerReview[]> GetBySubmissionIdAsync(
        Guid submissionId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .AsNoTracking()
            .Where(review => review.SubmissionId == submissionId)
            .OrderBy(review => review.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public Task<PeerReview[]> GetByReviewerStudentIdAsync(
        Guid reviewerStudentId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .AsNoTracking()
            .Where(review => review.ReviewerStudentId == reviewerStudentId)
            .OrderByDescending(review => review.CreatedAt)
            .ToArrayAsync(cancellationToken);
    }

    public Task<bool> HasReviewerAlreadyReviewedAsync(
        Guid submissionId,
        Guid reviewerStudentId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .AnyAsync(
                review => review.SubmissionId == submissionId &&
                          review.ReviewerStudentId == reviewerStudentId,
                cancellationToken);
    }

    public Task<int> CountApprovalsForSubmissionAsync(
        Guid submissionId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews
            .CountAsync(
                review => review.SubmissionId == submissionId && review.IsApproved,
                cancellationToken);
    }

    public Task AddAsync(PeerReview review, CancellationToken cancellationToken = default)
    {
        return dbContext.PeerReviews.AddAsync(review, cancellationToken).AsTask();
    }

    public Task UpdateAsync(PeerReview review, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        dbContext.PeerReviews.Update(review);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(PeerReview review, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        dbContext.PeerReviews.Remove(review);
        return Task.CompletedTask;
    }
}